using System.ComponentModel.DataAnnotations;

namespace RSU_360_X.ViewModels
{
    public class ConferenceVm
    {
        [Required, MaxLength(45)]
        public string MeetingName { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string ArticleTitle { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Authors { get; set; } = string.Empty;

        [Required]
        public string DayMonthYear { get; set; } = string.Empty; // yyyy-MM-dd

        [Required, Range(1900, 2200)]
        public int PublishYear { get; set; }

        [Required, MaxLength(45)]
        public string Country { get; set; } = string.Empty;

        [Required, MaxLength(4000)]
        public string Abstract { get; set; } = string.Empty;

        [Required, MaxLength(1000)]
        public string Keywords { get; set; } = string.Empty;
    }
}
