using System.Text.Json;

namespace RSU_360_X.Services
{
    public class JsonStorage
    {
        private readonly IWebHostEnvironment _env;

        public JsonStorage(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<List<T>> ReadListAsync<T>(string fileName)
        {
            var path = Path.Combine(_env.ContentRootPath, fileName);
            if (!File.Exists(path)) return new List<T>();

            var json = await File.ReadAllTextAsync(path);
            return JsonSerializer.Deserialize<List<T>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            }) ?? new List<T>();
        }
    }
}
