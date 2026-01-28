using Microsoft.EntityFrameworkCore;
using RSU_360_X.Models_Db;      // Scaffolded SQL Models
using RSU_360_X.Models.Auth;    // AppUser
using RSU_360_X.Models_core;    // Legacy JSON Models

namespace RSU_360_X.Services
{
    public class HybridAuthService : IHybridAuthService
    {
        private readonly JsonStorage _jsonStorage;
        private readonly EvDbContext _dbContext;

        public HybridAuthService(JsonStorage jsonStorage, EvDbContext dbContext)
        {
            _jsonStorage = jsonStorage;
            _dbContext = dbContext;
        }

        public async Task<AppUser?> LoginAsync(string inputUsername, string password)
        {
            // OOP Logic: Determine User Strategy based on input pattern
            bool isStudent = inputUsername.StartsWith("u", StringComparison.OrdinalIgnoreCase);

            // Normalize ID: Remove 'u' for database/json lookup
            string cleanId = isStudent ? inputUsername.Substring(1) : inputUsername;

            if (isStudent)
            {
                return await LoginStudentAsync(cleanId, password);
            }
            else
            {
                return await LoginStaffAsync(cleanId, password);
            }
        }

        // --- Encapsulated Staff Logic (SQL + JSON) ---
        private async Task<AppUser?> LoginStaffAsync(string empId, string password)
        {
            // 1. Verify Secret (JSON)
            var staffList = await _jsonStorage.ReadListAsync<StaffOrLecturerAuth>("auth.staff.json");
            var jsonUser = staffList.FirstOrDefault(u => u.Id == empId);

            if (jsonUser == null) return null; // User unknown

            // Password Check (Supports legacy hash or plain)
            bool isValid = VerifyPassword(password, jsonUser);
            if (!isValid) return null;

            // 2. Hydrate Profile (SQL)
            var sqlProfile = await _dbContext.Personnel
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.EmpId == empId);

            if (sqlProfile == null) return null; // Orphan account

            // 3. Construct Object
            return new AppUser
            {
                UserId = jsonUser.Id,
                // Map Security Fields
                PasswordHash = jsonUser.PasswordHash,
                PasswordSalt = jsonUser.PasswordSalt,

                EmpId = sqlProfile.EmpId,
                EmpType = sqlProfile.EmpType ?? "S",
                FirstName = sqlProfile.EmpFname ?? "",
                LastName = sqlProfile.EmpLname ?? "",
                Department = sqlProfile.EmpDepartment ?? "",
                Role = (sqlProfile.EmpType == "L") ? "Lecturer" : "Staff"
            };
        }

        // --- Encapsulated Student Logic (JSON Only) ---
        private async Task<AppUser?> LoginStudentAsync(string studentId, string password)
        {
            var list = await _jsonStorage.ReadListAsync<StudentAuth>("auth.students.json");
            var student = list.FirstOrDefault(s => s.Id == studentId);

            if (student == null) return null;

            if (!VerifyPassword(password, student)) return null;

            return new AppUser
            {
                UserId = student.Id,
                // Map Security Fields
                PasswordHash = student.PasswordHash,
                PasswordSalt = student.PasswordSalt,

                EmpType = "Student",
                FirstName = student.DisplayName ?? student.Id, // Students use DisplayName
                Role = "Student"
            };
        }

        // Helper Overloads for Password Verification
        private bool VerifyPassword(string input, dynamic userAuth)
        {
            string? hash = userAuth.PasswordHash;
            if (!string.IsNullOrEmpty(hash))
                return PasswordHasher.Verify(input, hash, userAuth.PasswordSalt, userAuth.PasswordIterations ?? 0);
            return userAuth.Password == input;
        }
    }
}
