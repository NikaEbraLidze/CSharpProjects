using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Entities
{
    public class Course
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public decimal Price { get; set; }
        public int InstructorId { get; set; }
        public Instructor Instructor { get; set; }
        public Review Review { get; set; }
        public List<StudentsCourses> StudentsCourses { get; set; }
    }
}