using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSU_360_X.Services
{
    public interface IStudentProfileRepository
    {
        Task<StudentProfileDto?> GetProfileAsync(string studentId);
    }

    public class StudentProfileDto
    {
        public string Id { get; set; } = ""; // Matches JSON "Id"
        public string DisplayName { get; set; } = "";
        public string Passport { get; set; } = ""; // Matches JSON "Passport"
        public string Nationality { get; set; } = "";
    }

    public class StudentProfileRepository : IStudentProfileRepository
    {
        private readonly IWebHostEnvironment _env;
        private string FilePath => Path.Combine(_env.ContentRootPath, "auth.students.json"); // Removed "Data" folder

        public StudentProfileRepository(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<StudentProfileDto?> GetProfileAsync(string studentId)
        {
            if (!File.Exists(FilePath)) return null;

            using var stream = File.OpenRead(FilePath);
            var allStudents = await JsonSerializer.DeserializeAsync<List<StudentProfileDto>>(stream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            
            return allStudents?.FirstOrDefault(x => x.Id == studentId);
        }
    }
}
