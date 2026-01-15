using ELearningPlatform.DTOS;
using ELearningPlatform.Entities;

using Microsoft.EntityFrameworkCore;

namespace ELearningPlatform.Repository
{
    public class PlatformRepository
    {
        private readonly AppDbContext _context;
        public PlatformRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Course> GetMostExpensiveCourse()
        {
            return await _context.Courses.OrderByDescending(c => c.Price).FirstOrDefaultAsync();
        }

        public async Task<List<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<List<Course>> GetCoursesWithDetails()
        {
            return await _context.Courses
                .Include(i => i.Instructor)
                .Include(sc => sc.StudentsCourses)
                .ThenInclude(s => s.Student)
                .ToListAsync();
        }

        public async Task<List<CourseDTO>> GetCourseDTOsAsync()
        {
            return await _context.Courses.Select(c => new CourseDTO
            {
                Id = c.Id,
                Title = c.Title,
                InstructorName = c.Instructor.FullName,
                StudentCount = c.StudentsCourses.Count()
            }).AsNoTracking().ToListAsync();
        }

        public async Task<List<InstructorDTO>> GetInstructorStats()
        {
            return await _context.Instructors.Select(i => new InstructorDTO
            {
                Id = i.Id,
                FullName = i.FullName,
                TotalCourses = i.Courses.Count(),
                AveragePrice = i.Courses.Average(c => c.Price)
            }).AsNoTracking().ToListAsync();
        }
    }
}