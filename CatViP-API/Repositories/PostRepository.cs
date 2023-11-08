using CatViP_API.Data;
using CatViP_API.DTOs;
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

        public ICollection<PostType> GetPostTypes()
        {
            return _context.PostTypes.ToList();
        }

        public async Task<bool> StorePost(Post post)
        {
            try
            {
                _context.Add(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
