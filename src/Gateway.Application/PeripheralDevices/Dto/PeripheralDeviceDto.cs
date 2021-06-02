namespace Gateway.PeripheralDevices.Dto
{
    using System;
    using Abp.Application.Services.Dto;
    using Abp.AutoMapper;
    using Gateway.Gates.Dto;
    using Gateway.Models;

    /// <summary>
    ///     The PeripheralDevice DTO.
    /// </summary>
    [AutoMapFrom(typeof(PeripheralDevice))]
    public class PeripheralDeviceDto : EntityDto<long>
    {
        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public long GateId { get; set; }
    }
}
