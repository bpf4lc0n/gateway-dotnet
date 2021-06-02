namespace Gateway.Gates.Dto
{
    using System.ComponentModel.DataAnnotations;

    using Abp.Application.Services.Dto;
    using Abp.AutoMapper;
    using Gateway.Models;

    /// <summary>
    ///     The update Gate input.
    /// </summary>
    [AutoMapTo(typeof(Gate))]
    public class UpdateGateInput : EntityDto<long>
    {
        [Required]
        public string Unique_serial_number { get; set; }

        [Required]
        public string Human_readable_name { get; set; }

        [Required]
        public string IPV4_address { get; set; }
    }
}
