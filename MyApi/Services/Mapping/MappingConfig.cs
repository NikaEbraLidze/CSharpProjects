using Mapster;

using MyApi.Entities;
using MyApi.Models.DTO;

namespace MyApi.Services.Mapping
{
    public static class MappingConfig
    {
        public static void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Comment, CommentDTO>();

            config.NewConfig<Topic, TopicDTO>()
                .Map(dest => dest.Comments,
                     src => (src.Comments ?? new List<Comment>()).Adapt<List<CommentDTO>>());

            config.NewConfig<Topic, GetTopicsDTO>();

            config.NewConfig<CreateTopicDTO, Topic>()
                .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
                .Map(dest => dest.Comments, _ => new List<Comment>());

            config.NewConfig<UpdateTopicDTO, Topic>();

            config.NewConfig<CreateCommentDTO, Comment>()
                .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow);

            config.NewConfig<UpdateCommentDTO, Comment>();
        }
    }
}
