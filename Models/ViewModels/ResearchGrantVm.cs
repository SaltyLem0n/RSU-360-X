using System.ComponentModel.DataAnnotations;

namespace RSU_360_X.ViewModels
{
    public class ResearchGrantVm
    {
        [Required, MaxLength(45)]
        public string ResearchTopic { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Position { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Sponsor { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string NumberOfYear { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string ContactPeriod { get; set; } = string.Empty;
    }
}
