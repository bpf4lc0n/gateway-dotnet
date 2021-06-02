using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Gateway.Models
{
    public class Gate : Entity<long>
    {
        [Required]
        public string Unique_serial_number { get; set; }
        [Required]
        public string Human_readable_name { get; set; }
        [Required]
        public string IPV4_address { get; set; }

        public List<PeripheralDevice> PeripheralDevices { get; set; }
    }
}
