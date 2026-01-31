using MyApi.Data;
using MyApi.Entities;

namespace MyApi.Repository
{
    public class TopicRepository : RepositoryBase<Topic, ApplicationDbContext>, ITopicRepository
    {
        public TopicRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}