namespace PeripheralDeviceway.PeripheralDevices
{
    using System.Threading.Tasks;

    using Abp.Application.Services;
    using Abp.Application.Services.Dto;
    using Gateway.PeripheralDevices.Dto;

    /// <summary>
    ///     The PeripheralDevice service interface.
    /// </summary>
    public interface IPeripheralDeviceAppService : IAsyncCrudAppService<PeripheralDeviceDto, long, PagedPeripheralDeviceResultRequestDto, CreatePeripheralDeviceDto, UpdatePeripheralDeviceInput>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gateId"></param>
        /// <returns></returns>
        Task<PagedResultDto<PeripheralDeviceDto>> GetAllByGateAsync(PagedPeripheralDeviceResultRequestDto input);

    }
}
