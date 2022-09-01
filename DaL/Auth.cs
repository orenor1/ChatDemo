
using DaL.Models;

namespace DaL
{
    public class Auth
    {
        private string path = "users/";
        public ChatUser Register(ChatUser chatUser)
        {
            DB db = DB.Instance;
            path += chatUser.Phone;

            ChatUser existUser = db.Get<ChatUser>(path);
            if(existUser != null && existUser.Phone == chatUser.Phone)
            {
                throw new Exception("User Exists !!");
            }

            return db.Set<ChatUser>(path, chatUser);
        }

        public ChatUser Login(ChatUser loginUser)
        {
            path += loginUser.Phone;
            DB db = DB.Instance;
            ChatUser user = db.Get<ChatUser>(path);

            if(user != null && user.Password != loginUser.Password)
            {
                throw new Exception("Incorrect Login !!");
            }

            return user;

        }
    }
}
