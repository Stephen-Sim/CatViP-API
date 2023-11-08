using CatViP_API.Data;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatViP_API.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly CatViPContext _context;
        public PostRepository(CatViPContext context)
        {
            this._context = context;
        }

        public IEnumerable<PostType> GetPostTypes()
        {
            return _context.PostTypes.ToList();
        }
    }
}
