using CatViP_API.Data;
using CatViP_API.DTOs.PostDTOs;
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

        public ICollection<Post> GetPosts()
        {
            return _context.Posts.Where(x => x.Status).Include(x => x.User).Include(x => x.MentionedCats).ThenInclude(x => x.Cat).ToList();
        }

        public int GetPostLikeCount(long postId)
        {
            return _context.Posts.Include(x => x.UserActions).FirstOrDefault(x => x.Id == postId)!.UserActions.Count(x => x.ActionTypeId == 1);
        }

        public int GetPostDisLikeCount(long postId)
        {
            return _context.Posts.Include(x => x.UserActions).FirstOrDefault(x => x.Id == postId)!.UserActions.Count(x => x.ActionTypeId == 2);
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

        public ICollection<Comment> GetPostComments(long postId)
        {
            return _context.Comments.Where(x => x.PostId == postId).Include(x => x.User).OrderByDescending(x => x.DateTime).ToList();
        }

        public ICollection<PostImage> GetPostImages(long postId)
        {
            return _context.PostImages.Where(x => x.PostId == postId).ToList();
        }

        public int GetCurrentUserStatusOnPost(long userId, long postId)
        {
            var post = _context.Posts.Include(x => x.UserActions).FirstOrDefault(x => x.Id == postId);

            if (post!.UserActions.Any(x => x.UserId == userId))
            {
                var userAction = post!.UserActions.FirstOrDefault(x => x.UserId == userId);
                return (int)userAction!.ActionTypeId;
            }

            return 0;
        }

        public int GetPostCommentCount(long postId)
        {
            return _context.Comments.Where(x => x.PostId == postId).Count();
        }

        public async Task<bool> ActPost(long userId, PostActionRequestDTO postActionDTO)
        {
            var postAction = await _context.UserActions.FirstOrDefaultAsync(x => x.UserId == userId && x.PostId == postActionDTO.PostId);

            try
            {
                if (postAction != null)
                {
                    postAction.ActionTypeId = postActionDTO.ActionTypeId;
                    _context.Update(postAction);
                }
                else
                {
                    var newPostAction = new UserAction
                    {
                        UserId = userId,
                        PostId = postActionDTO.PostId,
                        ActionTypeId = postActionDTO.ActionTypeId,
                    };

                    _context.Add(newPostAction);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CommentPost(long userId, CommentRequestDTO commentRequestDTO)
        {
            try
            {
                var comment = new Comment
                {
                    UserId = userId, 
                    PostId = commentRequestDTO.PostId,
                    DateTime = DateTime.Now,
                    Description = commentRequestDTO.Description
                };

                _context.Add(comment);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public ICollection<Post> GetOwnPosts(long userId)
        {
            return _context.Posts.Where(x => x.UserId == userId && x.Status).Include(x => x.MentionedCats).ThenInclude(x => x.Cat).ToList();
        }

        public ICollection<Post> GetPostsByCatId(long catId)
        {
            return _context.Posts.Where(x => x.MentionedCats.Any(y => y.CatId == catId) && x.Status).Include(x => x.MentionedCats).ThenInclude(x => x.Cat).ToList();
        }

        public bool CheckIfPostExist(long userId, long postId)
        {
            return _context.Posts.Any(x => x.UserId == userId && x.Id == postId && x.Status);
        }

        public async Task<bool> DeletePost(long postId)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(x => x.Id == postId);
                post!.Status = false;
                _context.Update(post);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> EditPost(long postId, EditPostRequestDTO editPostRequestDTO)
        {
            try
            {
                var post = _context.Posts.FirstOrDefault(x => x.Id == postId);
                post!.Description = editPostRequestDTO.Description;
                _context.Update(post);
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
