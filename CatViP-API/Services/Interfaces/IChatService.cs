using CatViP_API.DTOs.ChatDTOs;

namespace CatViP_API.Services.Interfaces
{
    public interface IChatService
    {
        ICollection<ChatDTO> GetChats(long authId, long userId);
        ICollection<ChatUserDTO> GetChatUsers(long authId);
    }
}
