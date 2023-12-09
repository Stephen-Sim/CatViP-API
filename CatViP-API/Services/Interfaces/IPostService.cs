﻿using CatViP_API.DTOs.PostDTOs;
using CatViP_API.Models;

namespace CatViP_API.Services.Interfaces
{
    public interface IPostService
    {
        ICollection<PostDTO> GetPosts(User currentUser);
        ICollection<PostDTO> GetOwnPosts(User currentUser);
        ICollection<PostDTO> GetPostsByCatId(long currentUserId, long catId);
        ICollection<PostTypeDTO> GetPostTypes(bool isExpert);
        Task<ResponseResult> CreatePost(User user, PostRequestDTO createPostDTO);
        Task<ResponseResult> ActPost(User user, PostActionRequestDTO postActionDTO);
        Task<ResponseResult> CommentPost(User user, CommentRequestDTO commentRequestDTO);
        ICollection<CommentDTO> GetPostComments(int postId);
        Task<ResponseResult> DeletePost(long id);
        Task<ResponseResult> EditPost(long id, EditPostRequestDTO editPostRequestDTO);
        ResponseResult CheckIfPostExist(long userId, long postId);
        Task<ResponseResult> DeleteActPost(long userId, long postId);
        ICollection<PostDTO> GetPostsByUserId(long userId);
    }
}
