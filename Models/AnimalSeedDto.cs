using System.Text.Json.Serialization;

namespace NTC_Zoo_API.Models
{
    public class AnimalSeedDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Species { get; set; }
        public string? Status { get; set; }
        public string? Health { get; set; }

        public LocationDto? Location { get; set; }

        public List<string>? FeedingSchedule { get; set; }

        public List<MaintenanceRecordDto>? MaintenanceRecords { get; set; }

        public string? Image { get; set; }
    }

    public class LocationDto
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class MaintenanceRecordDto
    {
        public string? Date { get; set; }
        public string? Task { get; set; }
    }
}