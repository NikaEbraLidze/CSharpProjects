using MapsterMapper;

using MyApi.Entities;
using MyApi.Models.DTO;
using MyApi.Repository;

namespace MyApi.Services
{
    public class TopicService : ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        private readonly IMapper _mapper;

        public TopicService(ITopicRepository topicRepository, IMapper mapper)
        {
            _topicRepository = topicRepository;
            _mapper = mapper;
        }

        public async Task<Guid> CreateTopicAsync(CreateTopicDTO model)
        {
            ValidateCreateTopicModel(model);

            var topicEntity = _mapper.Map<Topic>(model);
            await _topicRepository.AddAsync(topicEntity);
            await _topicRepository.SaveAsync();

            return topicEntity.Id;
        }

        public async Task<(List<GetTopicsDTO> topics, int totalCount)> GetAllTopicsAsync(int? pageNumber, int? pageSize)
        {
            ValidatePageParameters(pageNumber, pageSize);

            var result = await _topicRepository.GetAllAsync(pageNumber, pageSize, orderBy: "CreatedAt", ascending: false);

            return (_mapper.Map<List<GetTopicsDTO>>(result.Items), result.TotalCount);
        }

        public async Task<TopicDTO> GetTopicByIdAsync(Guid id)
        {
            ValidateGuid(id);

            var result = await _topicRepository.GetAsync(
                t => t.Id == id,
                includeProperties: "Comments",
                tracking: false
            );

            if (result == null) return null;

            return _mapper.Map<TopicDTO>(result);
        }

        public async Task<Guid> UpdateTopicAsync(UpdateTopicDTO model)
        {
            ValidateUpdateTopicModel(model);

            var topicEntity = _mapper.Map<Topic>(model);
            _topicRepository.Update(topicEntity);
            await _topicRepository.SaveAsync();

            return topicEntity.Id;
        }

        #region Validation Methods
        private static void ValidatePageParameters(int? pageNumber, int? pageSize)
        {
            if (pageNumber.HasValue && pageNumber <= 0)
                throw new ArgumentException("Page number must be greater than zero.");

            if (pageSize.HasValue && pageSize <= 0)
                throw new ArgumentException("Page size must be greater than zero.");
        }

        private static void ValidateGuid(Guid id)
        {
            if (id == Guid.Empty)
                throw new ArgumentException("Invalid Id provided.");
        }

        private static void ValidateCreateTopicModel(CreateTopicDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Reques body is required.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentException("Title is required.");
        }

        private void ValidateUpdateTopicModel(UpdateTopicDTO model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model), "Reques body is required.");

            if (string.IsNullOrWhiteSpace(model.Title))
                throw new ArgumentException("Title is required.");
        }
        #endregion
    }
}
