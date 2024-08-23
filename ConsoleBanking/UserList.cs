using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class UserList
    {
        Dictionary<string, User> users;
        public UserList()
        {
            users = new Dictionary<string, User>();
        }

        public void AddUser(User user)
        {
            if (user == null) throw new ArgumentNullException("user is null");
            users.Add(user.GetUsername(), user);
        }
        public void RemoveUser(User user) {
            if (!users.Remove(user.GetUsername())) throw new ArgumentException("User not found");
        }
        public User GetUser(string username)
        {
            if (users.TryGetValue(username, out User user)) return user;
            else throw new ArgumentException("User not found");
        }

        public IEnumerable<User> GetAllUsers()
        {
            return users.Values;
        }
    }
}
