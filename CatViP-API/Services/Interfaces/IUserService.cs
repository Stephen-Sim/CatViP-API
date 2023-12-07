using CatViP_API.DTOs.UserDTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface IUserService
    {
        Task<ResponseResult<UserInfoDTO>> GetUserInfoById(long id);
        ICollection<SearchUserDTO> SearchByUsenameOrFullName(string name, long authId);
    }
}
