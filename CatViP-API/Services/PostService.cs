using AutoMapper;
using Azure;
using CatViP_API.DTOs;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Text;

namespace CatViP_API.Services
{
    public class PostService : IPostService
    {
        private readonly IConfiguration _configuration;
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostService(IConfiguration configuration, IPostRepository postRepository, IMapper mapper)
        {
            _configuration = configuration;
            _postRepository = postRepository;
            _mapper = mapper;
        }

        public IEnumerable<PostTypeDTO> GetPostTypes()
        {
            var postTypes = _mapper.Map<IEnumerable<PostTypeDTO>>(_postRepository.GetPostTypes());

            return postTypes;
        }

        public async Task<ResponseResult> CreatePost(User user, CreatePostDTO createPostDTO)
        {
            var storeResult = new ResponseResult();

            if (createPostDTO.PostTypeId == 2 && user.RoleId != 3)
            {
                storeResult.IsSuccessful = false;
                storeResult.ErrorMessage = "user is not a expert";
                return storeResult;
            }

            if (createPostDTO.PostImages.IsNullOrEmpty())
            {
                storeResult.IsSuccessful = false;
                storeResult.ErrorMessage = "must be at least one image.";
                return storeResult;
            }

            if (createPostDTO.PostTypeId == 1)
            {
                foreach (var postImage in createPostDTO.PostImages)
                {
                    var isContainCat = await CheckIfPhotoContainCat(postImage.Image);

                    if (!isContainCat)
                    {
                        storeResult.IsSuccessful = false;
                        storeResult.ErrorMessage = "image may not contain cat.";
                        return storeResult;
                    }
                }
            }

            var post = new Post()
            {
                UserId = user.Id,
                PostTypeId = createPostDTO.PostTypeId,
                Description = createPostDTO.Description,
                DateTime = DateTime.Now,
                Status = true,

                PostImages = createPostDTO.PostImages.Select(pi => new PostImage
                {
                    Image = pi.Image,
                    IsBloodyContent = pi.IsBloodyContent
                }).ToList()
            };

            var storePostRes = await _postRepository.StorePost(post);

            if (!storePostRes)
            {
                storeResult.IsSuccessful = false;
                storeResult.ErrorMessage = "fail to store";
                return storeResult;
            }

            return storeResult;
        }

        public async Task<bool> CheckIfPhotoContainCat(byte[] image)
        {
            using (HttpClient client = new HttpClient())
            {
                var obj = new { image = image };
                var json = JsonConvert.SerializeObject(obj);

                var content = new StringContent(json, Encoding.UTF8, "application/json");

                var res = await client.PostAsync("http://127.0.0.1:5000/predict", content);

                if (res.IsSuccessStatusCode)
                {
                    string responseContent = await res.Content.ReadAsStringAsync();
                    var result = JsonConvert.DeserializeObject<dynamic>(responseContent);
                    return result!.result;
                }
            }

            return false;
        }
    }
}
