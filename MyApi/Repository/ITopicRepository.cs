using MyApi.Data;
using MyApi.Entities;

namespace MyApi.Repository
{
    public interface ITopicRepository : IRepositoryBase<Topic, ApplicationDbContext>
    {
    }
}