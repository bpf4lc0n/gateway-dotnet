namespace Gateway.Gates
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.Domain.Repositories;
    using Gateway.Gates.Dto;
    using Gateway.Models;
    using Microsoft.EntityFrameworkCore;

    using Abp.Linq.Extensions;
    using Abp.Extensions;
    using Gateway.Exceptions;
    using Gateway.Validation;
    using System;

    /// <summary>
    ///     The Gateway.Gates app service.
    /// </summary>
    [AbpAuthorize("Pages.Users")]
    public class GateAppService :
        AsyncCrudAppService<Gate, GateDto, long, PagedGateResultRequestDto, CreateGateDto, UpdateGateInput>,
        IGateAppService
    {
        /// <summary>
        ///     The Gateway.GatessRepository.
        /// </summary>
        private readonly IRepository<Gate, long> gatesRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gatesRepository"></param>
        public GateAppService(
            IRepository<Gate, long> gatesRepository)
            : base(gatesRepository)
        {
            this.gatesRepository = gatesRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override async Task<PagedResultDto<GateDto>> GetAllAsync(PagedGateResultRequestDto input)
        {
            var query = gatesRepository
                          .GetAllIncluding(vl => vl.PeripheralDevices)
                          .WhereIf(
                                 !input.Keyword.IsNullOrWhiteSpace(),
                                 x => x.Human_readable_name.ToLower().Contains(input.Keyword.ToLower()));

            query = ApplySorting(query, input);
            var totalItems = await query.CountAsync();
            var result = await query.ToListAsync();
            var mappedResult = new List<GateDto>();
            ObjectMapper.Map(result, mappedResult);
            var values = new PagedResultDto<GateDto>(totalItems, mappedResult);
            return values;
        }

        public async override Task<GateDto> CreateAsync(CreateGateDto input)
        {
            try
            {
                if (!ValidationHelper.IsIpv4(input.IPV4_address))
                    throw new Ipv4InvalidException("IPV4MustBeAtLeast7CharactersContainNumbersAndPoints");
                else
                {
                    var newGate = new Gate();
                    ObjectMapper.Map(input, newGate);
                    newGate = await gatesRepository.InsertAsync(newGate);
                    await UnitOfWorkManager.Current.SaveChangesAsync();

                    var result = new GateDto();
                    ObjectMapper.Map(newGate, result);
                    return result;
                }
            }
            catch (Ipv4InvalidException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // TODO > OVERRIDE EDIT GATE. ADD VALIDATION.
    }
}
