namespace TaskManagerAPI.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation property — one User has many Tasks
        public List<Task> Tasks { get; set; } = new List<Task>();
    }
}