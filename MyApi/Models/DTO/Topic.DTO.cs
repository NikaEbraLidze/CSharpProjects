using System.ComponentModel.DataAnnotations;

namespace MyApi.Models.DTO
{
    public record TopicDTO
    (
        Guid Id,
        string Title,
        string Description,
        string ImageUrl,
        DateTime CreatedAt,
        bool CommentsAreAllowed,
        List<CommentDTO> Comments
    );

    public record GetTopicsDTO
    (
        Guid Id,
        string Title,
        DateTime CreatedAt
    );

    public record CreateTopicDTO
    (
        [Required]
        string Title,
        string Description,
        string ImageUrl,
        bool CommentsAreAllowed
    );

    public record UpdateTopicDTO
    (
        [Required]
        Guid Id,
        [Required]
        string Title,
        string Description,
        string ImageUrl,
        bool CommentsAreAllowed
    );
}