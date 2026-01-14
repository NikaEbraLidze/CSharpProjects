using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Entities
{
    public class Instructor
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; }
        public List<Course> Courses { get; set; }
    }
}