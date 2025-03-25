using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Data
{
    public class TaskContext : DbContext
    {
        public TaskContext(DbContextOptions<TaskContext> options) : base(options)
        {
        }

        public DbSet<Models.Task> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed data
            modelBuilder.Entity<Models.Task>().HasData(
                new Models.Task { Id = "1", Title = "Task One", Description = "Install Angular CLI", Status = "pending" },
                new Models.Task { Id = "2", Title = "Task Two", Description = "Create a new Angular project", Status = "pending" },
                new Models.Task { Id = "3", Title = "Task Three", Description = "Set up project structure and routing", Status = "pending" },
                new Models.Task { Id = "4", Title = "Task Four", Description = "Install and configure Tailwind CSS", Status = "pending" },
                new Models.Task { Id = "5", Title = "Task Five", Description = "Create a reusable button component", Status = "pending" },
                new Models.Task { Id = "6", Title = "Task Six", Description = "Implement authentication with Firebase", Status = "pending" },
                new Models.Task { Id = "7", Title = "Task Seven", Description = "Set up a state management system with NgRx", Status = "completed" },
                new Models.Task { Id = "8", Title = "Task Eight", Description = "Fetch and display API data using HttpClient", Status = "pending" },
                new Models.Task { Id = "9", Title = "Task Nine", Description = "Create a dark mode toggle feature", Status = "completed" },
                new Models.Task { Id = "10", Title = "Task Ten", Description = "Optimize performance with lazy loading", Status = "pending" },
                new Models.Task { Id = "11", Title = "Task Eleven", Description = "Write unit tests for components and services", Status = "pending" },
                new Models.Task { Id = "12", Title = "Task Twelve", Description = "Deploy the Angular app to Firebase Hosting", Status = "completed" },
                new Models.Task { Id = "7534", Title = "Task Thirdteen", Description = "Develop the database with .NET", Status = "pending" }
            );
        }
    }
}
