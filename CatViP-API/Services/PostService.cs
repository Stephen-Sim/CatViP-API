using AutoMapper;
using CatViP_API.DTOs;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;

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

        public ResponseResult CreatePost(PostTypeDTO postTypeDTO)
        {
            return null;
        }
    }
}
