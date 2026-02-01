using System.ComponentModel.DataAnnotations;

namespace MyApi.Models.DTO
{
    public record CommentDTO
    (
        Guid Id,
        string Content,
        DateTime CreatedAt
    );

    public record CreateCommentDTO
    (
        [Required]
        string Content
    );

    public record UpdateCommentDTO
    (
        [Required]
        Guid Id,
        [Required]
        string Content
    );
}