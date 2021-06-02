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
    }
}
