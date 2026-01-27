using System;
using System.ComponentModel.DataAnnotations;

namespace RSU_360_X.ViewModels
{
    public class CreativeWorkVm
    {
        [Required, MaxLength(45)]
        public string QualityLevel { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public DateOnly DayMonthYear { get; set; }

        [Required, MaxLength(45)]
        public string Description { get; set; } = string.Empty;
    }
}
