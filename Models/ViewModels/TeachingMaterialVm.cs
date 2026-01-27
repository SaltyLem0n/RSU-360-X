using System;
using System.ComponentModel.DataAnnotations;

namespace RSU_360_X.ViewModels
{
    public class TeachingMaterialVm
    {
        [Required, MaxLength(45)]
        public string Subject { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string TeachingMaterial { get; set; } = string.Empty;

        [Required]
        public DateTime DayMonthYear { get; set; }

        [Required, MaxLength(45)]
        public string Type { get; set; } = string.Empty;

        [MaxLength(45)]
        public string? CoProducer { get; set; }
    }
}
