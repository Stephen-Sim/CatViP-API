using CatViP_API.Models;

namespace CatViP_API.Repositories.Interfaces
{
    public interface IChatRepository
    {
        ICollection<Chat> GetChats(long authId, long userId);
        ICollection<User> GetChatUsers(long authId);
    }
}
