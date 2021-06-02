namespace Gateway.PeripheralDevices.Dto
{
    using System;
    using Abp.AutoMapper;
    using Gateway.Models;

    /// <summary>
    ///     The create PeripheralDevice DTO.
    /// </summary>
    [AutoMapTo(typeof(PeripheralDevice))]
    public class CreatePeripheralDeviceDto
    {
        public int UID { get; set; }

        public string Vendor { get; set; }

        public DateTime DateCreated { get; set; }

        public string Status { get; set; }

        public long GateId { get; set; }
    }
}
