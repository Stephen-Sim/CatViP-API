using AutoMapper;
using CatViP_API.DTOs.ChatDTOs;
using CatViP_API.Helpers;
using CatViP_API.Models;
using CatViP_API.Repositories.Interfaces;
using CatViP_API.Services.Interfaces;

namespace CatViP_API.Services
{
    public class ChatService : IChatService
    {
        private readonly IChatRepository _chatRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public ChatService(IChatRepository chatRepository, IUserRepository userRepository, IMapper mapper)
        {
            _chatRepository = chatRepository;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public ICollection<ChatDTO> GetChats(long authId, long userId)
        {
            var chats = _chatRepository.GetChats(authId, userId);

            var chatDTOs = new List<ChatDTO>();

            foreach (var chat in chats)
            {
                chatDTOs.Add(new ChatDTO { Message = chat.Message, DateTime = chat.DateTime, IsCurrentUserSent = chat.UserSendId == authId });
            }

            return chatDTOs;
        }

        public ICollection<ChatUserDTO> GetChatUsers(long authId)
        {
            return _mapper.Map<ICollection<ChatUserDTO>>(_chatRepository.GetChatUsers(authId));
        }

        public async Task PushNotification(string sender, string receiver, string message)
        {
            var receiveUser = _userRepository.GetActiveCatOwnerOrExpertByUsername(receiver);
            var sendUser = _userRepository.GetActiveCatOwnerOrExpertByUsername(sender);

            if (receiveUser == null || sendUser == null)
            {
                return;
            }

            List<string> userTokens = new List<string>() { receiveUser.RememberToken!};

            await OneSignalSendNotiHelper.OneSignalSendChatNoti(userTokens, sendUser.FullName, message);
        }

        public async Task StoreChat(string sendUser, string receiveUser, string message)
        {
            await _chatRepository.StoreChat(sendUser, receiveUser, message);
        }
    }
}
