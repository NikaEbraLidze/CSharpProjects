using ELearningPlatform;
using ELearningPlatform.Repository;

class Program
{
    static async Task Main()
    {
        using var context = new AppDbContext();
        var repo = new PlatformRepository(context);

        Console.WriteLine("*******************************************");
        Console.WriteLine("");
        Console.WriteLine("*******************************************");

        var courses = await repo.GetCoursesWithDetails();
        foreach (var course in courses)
        {
            Console.WriteLine($"Course: {course.Title} | Price: {course.Price}");

            Console.WriteLine($"Instructor: {course.Instructor?.FullName}");

            Console.WriteLine("Students:");
            foreach (var sc in course.StudentsCourses)
            {
                Console.WriteLine($" - {sc.Student?.Name} ({sc.Student?.Email})");
            }

            Console.WriteLine(new string('-', 60));
        }

        Console.WriteLine("*******************************************");
        Console.WriteLine("");
        Console.WriteLine("*******************************************");

        var courseDTOs = await repo.GetCourseDTOsAsync();
        foreach (var dto in courseDTOs)
        {
            Console.WriteLine($"Id: {dto.Id}, Title: {dto.Title}, Instructor: {dto.InstructorName}, Student Count: {dto.StudentCount}");
        }

        Console.WriteLine("*******************************************");
        Console.WriteLine("");
        Console.WriteLine("*******************************************");

        var instructorStats = await repo.GetInstructorStats();
        foreach (var dto in instructorStats)
        {
            Console.WriteLine($"Id: {dto.Id}, FullName: {dto.FullName}, Total Course: {dto.TotalCourses}, Average Price: {dto.AveragePrice}");
        }

    }
}
