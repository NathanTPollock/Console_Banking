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
        private Dictionary<string, User> users;
        public UserList()
        {
            users = new Dictionary<string, User>();
        }
        /// <summary>
        /// Add a user to the user list dictionary with the username as the key.
        /// </summary>
        /// <param name="user"></param>
        public void AddUser(User user)
        {
            ArgumentNullException.ThrowIfNull(user);
            users.Add(user.GetUsername(), user);
        }
        /// <summary>
        /// Remove a user from the user list dictionary.
        /// </summary>
        /// <param name="user"></param>
        /// <exception cref="ArgumentException"></exception>
        public void RemoveUser(string username) {
            if (!users.Remove(username)) throw new ArgumentException("User not found");
        }
        /// <summary>
        /// Retrieves a User object with the passed username from the user list dictionary.
        /// </summary>
        /// <param name="username"></param>
        /// <returns>User object matching passed username</returns>
        /// <exception cref="ArgumentException"></exception>
        public User GetUser(string username)
        {
            if (users.TryGetValue(username, out User? user)) return user;
            else throw new ArgumentException("User not found");
        }
        /// <summary>
        /// Retrieves the number of users in the user list dictionary.
        /// </summary>
        /// <returns>Count of users in the user list</returns>
        public int Size()
        {
            return users.Count;
        }
        /// <summary>
        /// Retrieves all users in the user list dictionary.
        /// </summary>
        /// <returns>IEnumberable of all user values in the list</returns>
        public IEnumerable<User> GetAllUsers()
        {
            return users.Values;
        }
    }
}
