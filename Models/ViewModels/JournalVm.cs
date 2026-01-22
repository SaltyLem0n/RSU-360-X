using System.ComponentModel.DataAnnotations;

namespace RSU_360_X.ViewModels
{
    public class JournalVm
    {
        [Required, MaxLength(45)]
        public string ArticleTitle { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Author { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Publisher { get; set; } = string.Empty;

        [Required, Range(1900, 2200)]
        public int YearPublication { get; set; }

        [Required, Range(1, 12)]
        public int MonthPublication { get; set; }

        [Required, MaxLength(4000)]
        public string Abstract { get; set; } = string.Empty;

        [Required, MaxLength(1000)]
        public string Keywords { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Doi { get; set; } = string.Empty;
    }
}
