using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using System.Collections.Generic;

namespace ELearningPlatform
{
    public class AppDbContext : DbContext
    {
        private const string _connString =
            "Server=localhost\\SQLEXPRESS;Database=AppDbContext;Trusted_Connection=True;TrustServerCertificate=True;";

        DbSet<Entities.Course> Courses { get; set; }
        DbSet<Entities.Student> Students { get; set; }
        DbSet<Entities.StudentsCourses> StudentsCourses { get; set; }
        DbSet<Entities.Instructor> Instructors { get; set; }
        DbSet<Entities.Review> Reviews { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
            optionsBuilder.LogTo(Console.WriteLine, LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedInstructors(modelBuilder);
            SeedCourses(modelBuilder);
            SeedStudents(modelBuilder);
            SeedReviews(modelBuilder);
            SeedStudentsCourses(modelBuilder);
        }

        private void SeedInstructors(ModelBuilder modelBuilder)
        {
            var instructors = new List<Entities.Instructor>
            {
                new Entities.Instructor { Id = 1, FullName = "Nikoloz Chkhartishvili" },
                new Entities.Instructor { Id = 2, FullName = "John Doe" },
                new Entities.Instructor { Id = 3, FullName = "Jane Smith" }
            };
            modelBuilder.Entity<Entities.Instructor>().HasData(instructors);
        }

        private void SeedCourses(ModelBuilder modelBuilder)
        {
            var courses = new List<Entities.Course>
            {
                new Entities.Course { Id = 1, Title = "ASP.NET Core", Price = 2900.00m, InstructorId = 1 },
                new Entities.Course { Id = 2, Title = "C# Fundamentals", Price = 1500.00m, InstructorId = 2 },
                new Entities.Course { Id = 3, Title = "Entity Framework", Price = 2200.00m, InstructorId = 3 }
            };
            modelBuilder.Entity<Entities.Course>().HasData(courses);
        }

        private void SeedStudents(ModelBuilder modelBuilder)
        {
            var students = new List<Entities.Student>
            {
                new Entities.Student { Id = 1, Name = "Nika Ebralidze", Email = "nikaebralidze21@gmail.com" },
                new Entities.Student { Id = 2, Name = "Alice Johnson", Email = "alice.johnson@example.com" },
                new Entities.Student { Id = 3, Name = "Bob Wilson", Email = "bob.wilson@example.com" }
            };
            modelBuilder.Entity<Entities.Student>().HasData(students);
        }

        private void SeedReviews(ModelBuilder modelBuilder)
        {
            var reviews = new List<Entities.Review>
            {
                new Entities.Review { Id = 1, Content = "Great course on ASP.NET Core!", Rating = 5, CourseId = 1 },
                new Entities.Review { Id = 2, Content = "Very informative C# course.", Rating = 4, CourseId = 2 },
                new Entities.Review { Id = 3, Content = "Entity Framework made easy.", Rating = 5, CourseId = 3 }
            };
            modelBuilder.Entity<Entities.Review>().HasData(reviews);
        }

        private void SeedStudentsCourses(ModelBuilder modelBuilder)
        {
            var studentsCourses = new List<Entities.StudentsCourses>
            {
                new Entities.StudentsCourses { Id = 1, StudentId = 1, CourseId = 1 },
                new Entities.StudentsCourses { Id = 2, StudentId = 2, CourseId = 2 },
                new Entities.StudentsCourses { Id = 3, StudentId = 3, CourseId = 3 },
                new Entities.StudentsCourses { Id = 4, StudentId = 1, CourseId = 2 }
            };
            modelBuilder.Entity<Entities.StudentsCourses>().HasData(studentsCourses);
        }

    }
}