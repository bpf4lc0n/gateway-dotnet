namespace PeripheralDeviceway.PeripheralDevices
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;
    using Abp.Authorization;
    using Abp.Domain.Repositories;
    using Abp.Linq.Extensions;

    using Microsoft.EntityFrameworkCore;
    using Gateway.Models;
    using Gateway.PeripheralDevices.Dto;
    using System;
    using Gateway.Exceptions;

    /// <summary>
    ///     The PeripheralDevice app service.
    /// </summary>
    [AbpAuthorize]
    public class PeripheralDeviceAppService :
        AsyncCrudAppService<PeripheralDevice, PeripheralDeviceDto, long, PagedPeripheralDeviceResultRequestDto, CreatePeripheralDeviceDto, UpdatePeripheralDeviceInput>,
        IPeripheralDeviceAppService
    {
        /// <summary>
        ///     The PeripheralDevicesRepository.
        /// </summary>
        private readonly IRepository<PeripheralDevice, long> PeripheralDevicesRepository;


        /// <summary>
        /// 
        /// </summary>
        /// <param name="PeripheralDevicesRepository"></param>
        public PeripheralDeviceAppService(
            IRepository<PeripheralDevice, long> PeripheralDevicesRepository)
            : base(PeripheralDevicesRepository)
        {
            this.PeripheralDevicesRepository = PeripheralDevicesRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<PeripheralDeviceDto>> GetAllByGateAsync(PagedPeripheralDeviceResultRequestDto input)
        {           
            var query = PeripheralDevicesRepository
                             .GetAll()
                             .WhereIf(input.GateId.HasValue, x => x.GateId == input.GateId); 

            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            var totalItems = await query.CountAsync();
            var result = await query.ToListAsync();
            var mappedResult = new List<PeripheralDeviceDto>();
            ObjectMapper.Map(result, mappedResult);
            return new PagedResultDto<PeripheralDeviceDto>(totalItems, mappedResult);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async override Task<PeripheralDeviceDto> CreateAsync(CreatePeripheralDeviceDto input)
        {
            try
            {
                if (await PeripheralDevicesRepository.CountAsync(vl => vl.GateId == input.GateId) >= 10)
                    throw new ToManyPeripheralException("ToManyPeripheralException");
                else
                {
                    var newPeripheralDevice = new PeripheralDevice();
                    ObjectMapper.Map(input, newPeripheralDevice);
                    newPeripheralDevice.DateCreated = DateTime.Now;
                    newPeripheralDevice = await PeripheralDevicesRepository.InsertAsync(newPeripheralDevice);
                    await UnitOfWorkManager.Current.SaveChangesAsync();

                    var result = new PeripheralDeviceDto();
                    ObjectMapper.Map(newPeripheralDevice, result);
                    return result;
                }
            }
            catch (ToManyPeripheralException e)
            {
                throw e;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
