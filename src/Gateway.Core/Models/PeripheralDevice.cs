using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;

namespace Gateway.Models
{
    public class PeripheralDevice : Entity<long>
    {
        [Required]
        public int UID { get; set; }
        [Required]
        public string Vendor { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public long GateId { get; set; }

        public Gate Gate { get; set; }
    }
}
