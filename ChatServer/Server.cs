using DaL;
using DaL.Models;

namespace ChatServer
{
    public class Server
    {
        public ChatUser RegisterNewUser(ChatUser chatUser)
        {
            Auth auth = new();
            return auth.Register(chatUser);
        }

        public ChatUser Login(ChatUser chatUser)
        {
            Auth auth = new();
            return auth.Login(chatUser);
        }

        public bool ConnectToChat(ChatUser chatUser)
        {
            LiveChat chat = LiveChat.Instance;
            return chat.ConnectToChat(chatUser);
        }

        public bool SendMessage(ChatUser user, string message)
        {
            LiveChat chat = LiveChat.Instance;
            return chat.SendMessage(user, message);
        }

        public void StartListen(Action<ChatMessage> callback)
        {
            LiveChat chat = LiveChat.Instance;
            chat.Listen(callback);
        }

        public bool Disconnect(ChatUser user)
        {
            LiveChat chat = LiveChat.Instance;
            return chat.Disconnect(user);
        }
    }
}