namespace NTC_Zoo_API.Models
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
        public string Health { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public string FeedingSchedule { get; set; } = string.Empty;
        public string MaintenanceNotes { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}