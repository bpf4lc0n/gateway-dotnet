namespace Gateway.Gates.Dto
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Abp.Application.Services.Dto;
    using Abp.AutoMapper;
    using Gateway.Models;
    using Gateway.PeripheralDevices.Dto;

    /// <summary>
    ///     The Gate DTO.
    /// </summary>
    [AutoMapFrom(typeof(Gate))]
    public class GateDto : EntityDto<long>
    {
        [Required]
        public string Unique_serial_number { get; set; }

        [Required]
        public string Human_readable_name { get; set; }

        [Required]
        public string IPV4_address { get; set; }

        public List<PeripheralDeviceDto> PeripheralDevices { get; set; }
    }
}
