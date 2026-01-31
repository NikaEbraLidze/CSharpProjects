using MyApi.Data;
using MyApi.Entities;

namespace MyApi.Repository
{
    public class CommentRepository : RepositoryBase<Comment, ApplicationDbContext>, ICommentRepository
    {
        public CommentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}