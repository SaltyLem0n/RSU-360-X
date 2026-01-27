using Microsoft.AspNetCore.Hosting;
using RSU_360_X.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace RSU_360_X.Services
{
    public interface IContactRepository
    {
        Task<bool> NeedsEmailUpdateAsync(string studentId);
        Task UpdateEmailAsync(string studentId, string email);
    }

    public class ContactRepository : IContactRepository
    {
        private readonly IWebHostEnvironment _env;
        private string FilePath => Path.Combine(_env.ContentRootPath, "Data", "student_contacts.json");

        public ContactRepository(IWebHostEnvironment env)
        {
            _env = env;
            var dir = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir!);
        }

        public async Task<bool> NeedsEmailUpdateAsync(string studentId)
        {
            var all = await GetAllAsync();
            var contact = all.FirstOrDefault(x => x.StudentId == studentId);

            // Logic: Show modal if no record OR last update was > 1 year ago
            if (contact == null) return true;
            if (contact.LastUpdated < DateTime.UtcNow.AddYears(-1)) return true;
            
            return false;
        }

        public async Task UpdateEmailAsync(string studentId, string email)
        {
            var all = await GetAllAsync();
            var contact = all.FirstOrDefault(x => x.StudentId == studentId);

            if (contact != null)
            {
                contact.Email = email;
                contact.LastUpdated = DateTime.UtcNow;
            }
            else
            {
                all.Add(new StudentContact 
                { 
                    StudentId = studentId, 
                    Email = email, 
                    LastUpdated = DateTime.UtcNow 
                });
            }

            var json = JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        private async Task<List<StudentContact>> GetAllAsync()
        {
            if (!File.Exists(FilePath)) return new List<StudentContact>();
            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<StudentContact>>(json) ?? new List<StudentContact>();
        }
    }
}
