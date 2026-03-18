using System.Text.Json.Serialization;

namespace TaskManagerAPI.Models
{
    public class Task
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsDone { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property — links back to User
        // JsonIgnore = don't require this when receiving POST data
        [JsonIgnore]
        public User? User { get; set; }
    }
}