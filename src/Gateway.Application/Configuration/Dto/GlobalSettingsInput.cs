﻿using System.ComponentModel.DataAnnotations;

namespace Gateway.Configuration.Dto
{
    public class GlobalSettingsInput
    {
        [Required]
        [StringLength(32)]
        public string AppName { get; set; }
    }
}
