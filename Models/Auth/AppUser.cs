namespace RSU_360_X.Models.Auth
{
    public class AppUser
    {
        // Identity
        public string UserId { get; set; } = string.Empty;
        public string UserStatus { get; set; } = "Active";

        // Security (Added per request)
        public string? PasswordHash { get; set; }
        public string? PasswordSalt { get; set; }

        // Profile (SQL/JSON)
        public string EmpId { get; set; } = string.Empty;
        public string EmpType { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Authorization
        public string Role { get; set; } = "Guest";

        public string GetDisplayName()
        {
            if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
                return UserId;
            return $"{FirstName} {LastName}".Trim();
        }
    }
}
