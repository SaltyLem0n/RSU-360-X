using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RSU_360_X.Models;

namespace RSU_360_X.Services
{
    public interface IVisaRepository
    {
        Task<VisaSubmission?> GetByStudentAsync(string studentId);
        Task<List<VisaSubmission>> GetAllAsync();
        Task SaveAsync(VisaSubmission sub);
    }

    public class VisaRepository : IVisaRepository
    {
        private readonly IWebHostEnvironment _env;
        private string FilePath => Path.Combine(_env.ContentRootPath, "Data", "visa_submissions.json");

        public VisaRepository(IWebHostEnvironment env)
        {
            _env = env;
            var dir = Path.GetDirectoryName(FilePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir!);
        }

        public async Task<VisaSubmission?> GetByStudentAsync(string studentId)
        {
            var all = await GetAllAsync();
            return all.FirstOrDefault(x => x.StudentId == studentId);
        }

        public async Task SaveAsync(VisaSubmission sub)
        {
            var all = await GetAllAsync();
            var idx = all.FindIndex(x => x.StudentId == sub.StudentId);
            
            if (idx >= 0) all[idx] = sub;
            else all.Add(sub);

            var json = JsonSerializer.Serialize(all, new JsonSerializerOptions { WriteIndented = true });
            await File.WriteAllTextAsync(FilePath, json);
        }

        public async Task<List<VisaSubmission>> GetAllAsync()
        {
            if (!File.Exists(FilePath)) return new List<VisaSubmission>();
            var json = await File.ReadAllTextAsync(FilePath);
            return JsonSerializer.Deserialize<List<VisaSubmission>>(json) ?? new List<VisaSubmission>();
        }
    }
}
