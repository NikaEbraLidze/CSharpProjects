using MyApi.Models.DTO;

namespace MyApi.Services
{
    public interface ITopicService
    {
        Task<(List<GetTopicsDTO> topics, int totalCount)> GetAllTopicsAsync(int? pageNumber,
            int? pageSize);
        Task<TopicDTO> GetTopicByIdAsync(Guid id);
        Task<int> CreateTopicAsync(CreateTopicDTO model);
        Task<int> UpdateTopicAsync(UpdateTopicDTO model);
    }
}