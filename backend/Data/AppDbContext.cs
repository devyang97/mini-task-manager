using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Models;

namespace TaskManagerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        // These two lines = two MySQL tables
        public DbSet<User> Users { get; set; }
        public DbSet<TaskManagerAPI.Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Tell EF Core exact MySQL column names
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });

            modelBuilder.Entity<TaskManagerAPI.Models.Task>(entity =>
            {
                entity.ToTable("Tasks");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.IsDone).HasColumnName("is_done");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            });
        }
    }
}