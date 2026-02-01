using Mapster;

using MyApi.Entities;
using MyApi.Models.DTO;

namespace MyApi.Services.Mapping
{
    public static class MappingConfig
    {
        public static void Register(TypeAdapterConfig config)
        {
            config.NewConfig<Topic, TopicDTO>();
            config.NewConfig<Topic, GetTopicsDTO>();

            config.NewConfig<CreateTopicDTO, Topic>()
                .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow)
                .Map(dest => dest.Comments, _ => new List<Comment>());

            config.NewConfig<UpdateTopicDTO, Topic>();

            config.NewConfig<Comment, CommentDTO>();

            config.NewConfig<CreateCommentDTO, Comment>()
                .Map(dest => dest.CreatedAt, _ => DateTime.UtcNow);

            config.NewConfig<UpdateCommentDTO, Comment>();
        }
    }
}
