namespace Gateway.Gates.Dto
{
    using System.ComponentModel.DataAnnotations;
    using Abp.AutoMapper;
    using Gateway.Models;

    /// <summary>
    ///     The create Gate DTO.
    /// </summary>
    [AutoMapTo(typeof(Gate))]
    public class CreateGateDto
    {
        [Required]
        public string Unique_serial_number { get; set; }

        [Required]
        public string Human_readable_name { get; set; }

        [Required]
        public string IPV4_address { get; set; }
    }
}
