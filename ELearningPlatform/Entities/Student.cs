using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Entities
{
    public class Student
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        public List<StudentsCourses> StudentsCourses { get; set; }
    }
}