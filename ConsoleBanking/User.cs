using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleBanking
{
    internal class User
    {
        private string username;
        private string password;
        private string name;
        private string address;
        // private AccountList accounts;

        //Constructor
        public User(string username, string password, string name, string address)
        {
            this.username = username;
            this.password = password;
            this.name = name;
            this.address = address;
        }

        // Getters
        public string GetUsername() => username;
        public string GetPassword() => password;
        public string GetName() => name;
        public string GetAddress() => address;

        // Setters
        public void SetUsername(string username) => this.username = username;
        public void SetPassword(string password) => this.password = password;
        public void SetName(string name) => this.name = name;
        public void SetAddress(string address) => this.address = address;

    }
}
