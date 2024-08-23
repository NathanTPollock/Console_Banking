using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class UserList
    {
        List<User> users;
        public UserList()
        {
            users = new List<User>();
        }

        public void AddUser(User user)
        {
            users.Add(user);
        }
        public void RemoveUser(User user) {
            users.Remove(user);
        }
        public User GetUser(string username)
        {
            foreach (User user in users)
            {
                if (user.GetUsername() == username)
                {
                    return user;
                }
            }
            throw new ArgumentException("User not found");
        }
    }
}
