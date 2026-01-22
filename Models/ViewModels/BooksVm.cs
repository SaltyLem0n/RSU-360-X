using System.ComponentModel.DataAnnotations;

namespace RSU_360_X.ViewModels
{
    public class BooksVm
    {
        [Required, MaxLength(45)]
        public string NameOfWork { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string TeachingBook { get; set; } = string.Empty;

        [Required, MaxLength(45)]
        public string Type { get; set; } = string.Empty;

        [Required]
        public string DayMonthYear { get; set; } = string.Empty;
    }
}
