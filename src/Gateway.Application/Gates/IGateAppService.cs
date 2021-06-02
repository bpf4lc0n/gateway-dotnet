namespace Gateway.Gates
{
    using Abp.Application.Services;
    using Gateway.Gates.Dto;

    /// <summary>
    ///     The GateAppService interface.
    /// </summary>
    public interface IGateAppService : IAsyncCrudAppService<GateDto, long, PagedGateResultRequestDto, CreateGateDto,
        UpdateGateInput>
    {
    }
}
