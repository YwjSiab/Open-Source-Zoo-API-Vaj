using System.Text.Json;
using NTC_Zoo_API.Models;

namespace NTC_Zoo_API.Data
{
    public static class JsonAnimalSeeder
    {
        public static async Task SeedAsync(ZooContext context, IWebHostEnvironment environment)
        {
            if (context.Animals.Any())
            {
                return;
            }

            string filePath = Path.Combine(environment.ContentRootPath, "SeedData", "animals.json");

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"Could not find seed file at: {filePath}");
            }

            string json = await File.ReadAllTextAsync(filePath);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            List<AnimalSeedDto>? dtos = JsonSerializer.Deserialize<List<AnimalSeedDto>>(json, options);

            if (dtos == null)
                return;

            List<Animal> animals = dtos.Select(dto => new Animal
            {
                Name = dto.Name ?? "Unknown",
                Species = dto.Species ?? "Unknown",
                Status = dto.Status ?? "Unknown",
                Health = dto.Health ?? "Unknown",

                Location = dto.Location != null
                    ? $"{dto.Location.Lat}, {dto.Location.Lng}"
                    : "Unknown",

                FeedingSchedule = dto.FeedingSchedule != null
                    ? string.Join(", ", dto.FeedingSchedule)
                    : "",

                MaintenanceNotes = dto.MaintenanceRecords != null
                    ? string.Join(" | ", dto.MaintenanceRecords
                        .Select(r => $"{r.Date}: {r.Task}"))
                    : "",

                ImageUrl = NormalizeImagePath(dto.Image)
            }).ToList();

            context.Animals.AddRange(animals);
            await context.SaveChangesAsync();
        }

        private static string NormalizeImagePath(string? path)
        {
            if (string.IsNullOrWhiteSpace(path))
                return "/images/default.png";

            string cleaned = path.Replace("\\", "/").Trim();

            if (!cleaned.StartsWith("/"))
                cleaned = "/" + cleaned;

            return cleaned;
        }
    }
}