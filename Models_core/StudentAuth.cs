namespace RSU_360_X.Models_core
{
    public class StudentAuth
    {
        public string? Id { get; set; }
        public string? Password { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public int? PasswordIterations { get; set; }
        public string? PasswordAlgo { get; set; }
        public string? DisplayName { get; set; }
        public string? Passport { get; set; }
        public string? Nationality { get; set; }
        public string? SchoolEmail { get; set; }
        public string? PersonalEmail { get; set; }
        public int? PersonalEmailConfirmedYear { get; set; }
    }
}
