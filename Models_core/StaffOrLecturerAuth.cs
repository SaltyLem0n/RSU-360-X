namespace RSU_360_X.Models_core
{
    public class StaffOrLecturerAuth
    {
        public string? Id { get; set; }
        public string? Password { get; set; }
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }
        public int? PasswordIterations { get; set; }
        public string? PasswordAlgo { get; set; }
    }
}
