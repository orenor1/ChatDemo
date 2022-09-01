using DaL.Models;

namespace DaL
{
    public class LiveChat
    {
        private static LiveChat instance = new LiveChat();
        DB db = DB.Instance;
        string channelId = string.Empty;
        string path = "chats/";
        bool connected = false;

        private LiveChat()
        {
            channelId = Guid.NewGuid().ToString();
        }

        public static LiveChat Instance
        {
            get { return instance; }
        }

        public bool ConnectToChat(ChatUser user)
        {
            string fullPath = $"{path}{channelId}/users/{user.Phone}";
            ChatUser existUser = db.Get<ChatUser>(fullPath);
            if (existUser == null)
            {
                try
                {
                    db.Set<ChatUser>(fullPath, user);
                    connected = true;   
                }
                catch
                {
                    return false;
                }
                
            }
            return true;
        }

        public bool SendMessage(ChatUser user, string message)
        {
            ChatMessage newMessage = new()
            {
                body = Reverse(message),
                userPhone = user.Phone
            };

            string fullPath = $"{path}{channelId}/messeges";
            try
            {
                db.Set<ChatMessage>(fullPath, newMessage);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public async void Listen(Action<ChatMessage> callback) {

            string fullPath = $"{path}{channelId}/messeges";
           
            while (connected) {
                await Task.Delay(2000);
                var messages = db.Get<ChatMessage>(fullPath);
                callback(messages);
            }
        }

        private string Reverse(string s)
        {
            char[] charArray = s.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public bool Disconnect(ChatUser user)
        {
            connected = false;
            bool deleted = false;
            string fullPath = $"{path}{channelId}/users/{user.Phone}";
            ChatUser existUser = db.Get<ChatUser>(fullPath);
            if (existUser == null)
            {
                try
                {
                    db.Delete(fullPath);
                    deleted = true;
                }
                catch
                {
                    return deleted;
                }

            }
            return deleted;
        }
    }
}
