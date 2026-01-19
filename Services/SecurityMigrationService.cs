using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Hosting;
using RSU_360_X.Models_core;
using System.Text.Json;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RSU_360_X.Services
{
    public class SecurityMigrationService : BackgroundService
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<SecurityMigrationService> _logger;

        public SecurityMigrationService(IWebHostEnvironment env, ILogger<SecurityMigrationService> logger)
        {
            _env = env;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Security Migration Service starting...");

            try
            {
                await MigrateStaffAsync();
                await MigrateStudentsAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during Security Migration.");
            }
        }

        private async Task MigrateStaffAsync()
        {
            var staffPath = Path.Combine(_env.ContentRootPath, "auth.staff.json");
            if (File.Exists(staffPath))
            {
                var json = await File.ReadAllTextAsync(staffPath);
                var staffList = JsonSerializer.Deserialize<List<StaffOrLecturerAuth>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (staffList != null)
                {
                    bool changed = false;
                    int updatedCount = 0;
                    foreach (var staff in staffList)
                    {
                        if (!string.IsNullOrEmpty(staff.Password))
                        {
                            PasswordHasher.HashPassword(staff.Password, out string hash, out string salt);
                            staff.PasswordHash = hash;
                            staff.PasswordSalt = salt;
                            staff.PasswordIterations = 50000;
                            staff.PasswordAlgo = "pbkdf2-sha256";
                            staff.Password = null; // Clear plain text
                            updatedCount++;
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        var newJson = JsonSerializer.Serialize(staffList, new JsonSerializerOptions { WriteIndented = true });
                        await File.WriteAllTextAsync(staffPath, newJson);
                        _logger.LogInformation($"Migrated {updatedCount} staff passwords.");
                    }
                }
            }
        }

        private async Task MigrateStudentsAsync()
        {
            var studentPath = Path.Combine(_env.ContentRootPath, "auth.students.json");
            if (File.Exists(studentPath))
            {
                var json = await File.ReadAllTextAsync(studentPath);
                var studentList = JsonSerializer.Deserialize<List<StudentAuth>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (studentList != null)
                {
                    bool changed = false;
                    int updatedCount = 0;
                    foreach (var student in studentList)
                    {
                        if (!string.IsNullOrEmpty(student.Password))
                        {
                            PasswordHasher.HashPassword(student.Password, out string hash, out string salt);
                            student.PasswordHash = hash;
                            student.PasswordSalt = salt;
                            student.PasswordIterations = 50000;
                            student.PasswordAlgo = "pbkdf2-sha256";
                            student.Password = null; // Clear plain text
                            updatedCount++;
                            changed = true;
                        }
                    }

                    if (changed)
                    {
                        var newJson = JsonSerializer.Serialize(studentList, new JsonSerializerOptions { WriteIndented = true });
                        await File.WriteAllTextAsync(studentPath, newJson);
                        _logger.LogInformation($"Migrated {updatedCount} student passwords.");
                    }
                }
            }
        }
    }
}
