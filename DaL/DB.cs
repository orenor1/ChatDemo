using FireSharp;
using FireSharp.Config;
using FireSharp.Interfaces;
using FireSharp.Response;
using System.Security.Permissions;

namespace DaL
{
    public class DB
    {
        private static DB instance = new DB();
        IFirebaseConfig firebaseConfig;
        FirebaseClient firebaseClient;
        private DB()
        {
            firebaseConfig = new FirebaseConfig()
            {
                AuthSecret = "aMvcyTsAzUeoBqz3lYpAAoDyyzgJlOKbQr13ZRa5",
                BasePath = "https://chatdemo-8893b-default-rtdb.firebaseio.com/"
            };

            firebaseClient = new FirebaseClient(firebaseConfig);
        }

        public static DB Instance
        {
            get { return instance; }
        }

        public T Set<T>(string path, T value)
        {
            SetResponse response = firebaseClient.Set(path, value);
            var result = response.ResultAs<T>();

            return result;
        }

        public T Get<T>(string path)
        {
            T result;
            try
            {
                FirebaseResponse response = firebaseClient.Get(path);
                result = response.ResultAs<T>();
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public bool Delete(string path)
        {
           bool deleted = false;
            try
            {
               firebaseClient.Delete(path);
                deleted = true;
            }
            catch (Exception e)
            {
                throw e;
            }

            return deleted;
        }

    }
}