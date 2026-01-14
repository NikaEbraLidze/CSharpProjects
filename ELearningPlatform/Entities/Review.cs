using System.ComponentModel.DataAnnotations;

namespace ELearningPlatform.Entities
{
    public class Review
    {
        public int Id { get; set; }
        [Required]
        public string Content { get; set; }
        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}