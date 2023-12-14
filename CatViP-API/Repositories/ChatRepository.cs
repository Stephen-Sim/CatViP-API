using CatViP_API.Data;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CatViP_API.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly CatViPContext _context;

        public ChatRepository(CatViPContext context)
        {
            _context = context;
        }

        public ICollection<Chat> GetChats(long authId, long userId)
        {
            return _context.Chats
                .Where(c => (c.UserSendId == authId && c.UserReceiveId == userId) || 
                            (c.UserSendId == userId && c.UserReceiveId == authId))
                .OrderByDescending(c => c.DateTime)
                .ToList();
        }

        public ICollection<User> GetChatUsers(long authId)
        {
            return _context.Chats
                .Include(x => x.UserSend).Include(x => x.UserReceive)
                .ToList()
                .Where(c => c.UserSendId == authId || c.UserReceiveId == authId)
                .Select(c => new {
                    User = c.UserSendId == authId ? c.UserReceive : c.UserSend,
                    LastChatDate = c.DateTime
                })
                .GroupBy(x => x.User.Id)
                .Select(x => new {
                    User = x.First().User,
                    LastChatDate = x.Max(y => y.LastChatDate)
                })
                .OrderByDescending(x => x.LastChatDate)
                .Select(x => x.User)
                .ToList();
        }

        public Chat GetLastestChat(long authId, long userId)
        {
            return _context.Chats
                .Include(x => x.UserSend).Include(x => x.UserReceive)
                .ToList()
                .Where(c => (c.UserSendId == authId && c.UserReceiveId == userId) || (c.UserSendId == userId && c.UserReceiveId == authId))
                .OrderByDescending(x => x.DateTime)
                .First();
        }

        public async Task StoreChat(string sendUser, string receiveUser, string message)
        {
            try
            {
                var chat = new Chat()
                {
                    DateTime = DateTime.Now,
                    Message = message,
                    UserSendId = _context.Users.First(x => x.Username == sendUser).Id,
                    UserReceiveId = _context.Users.First(x => x.Username == receiveUser).Id,
                };

                _context.Add(chat);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
            }
        }
    }
}
