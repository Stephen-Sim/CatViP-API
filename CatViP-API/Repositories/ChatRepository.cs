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
                .Where(c => (c.UserChat.UserSendId == authId && c.UserChat.UserReceiveId == userId) || 
                            (c.UserChat.UserSendId == userId && c.UserChat.UserReceiveId == authId))
                .OrderByDescending(c => c.DateTime)
                .ToList();
        }

        public ICollection<User> GetChatUsers(long authId)
        {
            return _context.Chats
                .Include(x => x.UserChat.UserSend).Include(x => x.UserChat.UserReceive)
                .ToList()
                .Where(c => c.UserChat.UserSendId == authId || c.UserChat.UserReceiveId == authId)
                .Select(c => new {
                    User = c.UserChat.UserSendId == authId ? c.UserChat.UserReceive : c.UserChat.UserSend,
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
                .Include(x => x.UserChat.UserSend).Include(x => x.UserChat.UserReceive)
                .ToList()
                .Where(c => (c.UserChat.UserSendId == authId && c.UserChat.UserReceiveId == userId) || (c.UserChat.UserSendId == userId && c.UserChat.UserReceiveId == authId))
                .OrderByDescending(x => x.DateTime)
                .First();
        }

        public async Task StoreChat(string sendUser, string receiveUser, string message)
        {
            try
            {
                var userChat = _context.UserChats.FirstOrDefault(x => x.UserSend.Username == sendUser && x.UserReceive.Username == receiveUser);

                if (userChat == null)
                {
                    userChat = new UserChat()
                    {
                        LastSeen = DateTime.Now,
                        UserReceiveId = _context.Users.First(x => x.Username == receiveUser).Id,
                        UserSendId = _context.Users.First(x => x.Username == sendUser).Id,
                    };

                    _context.Add(userChat);

                    var userChat1 = new UserChat()
                    {
                        LastSeen = DateTime.Now,
                        UserSendId = _context.Users.First(x => x.Username == receiveUser).Id,
                        UserReceiveId = _context.Users.First(x => x.Username == sendUser).Id,
                    };

                    _context.Add(userChat1);

                    await _context.SaveChangesAsync();
                }

                var chat = new Chat()
                {
                    DateTime = DateTime.Now,
                    Message = message,
                    UserChatId = userChat.Id
                };

                _context.Add(chat);
                await _context.SaveChangesAsync();
            }
            catch (Exception err)
            {
                await Console.Out.WriteLineAsync(err.Message);
            }
        }
    }
}
