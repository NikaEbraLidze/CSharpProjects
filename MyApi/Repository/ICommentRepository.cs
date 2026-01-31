using MyApi.Data;
using MyApi.Entities;

namespace MyApi.Repository
{
    public interface ICommentRepository : IRepositoryBase<Comment, ApplicationDbContext>
    {
    }
}