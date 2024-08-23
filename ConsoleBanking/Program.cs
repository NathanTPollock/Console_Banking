using System.Net.Security;

namespace ConsoleBanking
{
    class Program
    {
        static UserList users = new UserList();
        static User? currentUser;
        static void Main(string[] args)
        {
            // Send user to main page to login or create account
            bool loggedIn;
            loggedIn = MainPage();
            if(!loggedIn) return;
            // User is logged in
            try
            {
                UserPage();
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        static bool MainPage()
        {
            string input;
            bool exit = false;
            bool loggedIn = false;
            while (!exit && !loggedIn)
            {
                // Welcome User, Prompt for login or create account
                Console.Write("Welcome to Console Banking! Would you like to login or create an account? (login/create/exit): ");
                input = Console.ReadLine();
                input = input.ToLower();
                switch (input)
                {
                    case "login":
                        // Login
                        Console.WriteLine("\nDirecting to login page...\n");
                        loggedIn = UserLogin();
                        break;
                    case "create":
                        // Create Account
                        Console.WriteLine("\nDirecting to create account page...\n");
                        CreateUser();
                        break;
                    case "exit":
                        Console.WriteLine("\nExiting Console Banking. Goodbye!\n");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine(input + " is an invalid command. Please try again.\n");
                        break;
                }
            }
            return loggedIn;
        }

        static void UserPage()
        {
            if (currentUser == null) throw new Exception("currentUser is null.");
            Console.WriteLine("Hello, " + currentUser.GetName() + "!");
            Console.ReadKey();
        }
        static bool UserLogin()
        {
            string username;
            string password;
            
            // Get username and password
            Console.Write("Enter your username: ");
            username = Console.ReadLine();
            Console.Write("Enter your password: ");
            password = Console.ReadLine();

            // Check if user exists and password is valid
            try
            {
                User user = users.GetUser(username);
                if (user.GetPassword() == password)
                {
                    Console.WriteLine("Login successful!\n");
                    currentUser = user;
                    return true;
                }
                else
                {
                    Console.WriteLine("Incorrect password.\n");
                    return false;
                }
            }
            catch (ArgumentException)
            {
                Console.WriteLine("User not found.\n");
                return false;
            }
            
        }

        static void CreateUser()
        {
            string username = "default";
            string password = "default";
            string confirmPassword = "def";
            bool userExists = true;

            // Ensure username is unique
            while (userExists)
            {
                Console.Write("Enter your desired username: ");
                username = Console.ReadLine();

                // Check if username already exists
                try
                {
                    users.GetUser(username);
                    Console.WriteLine("User already exists. Try logging in.\n");
                    return;
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Username is available.");
                    userExists = false;
                }
            }
            
            // Get and confirm password
            while (password != confirmPassword)
            {
                Console.Write("Enter your desired password: ");
                password = Console.ReadLine();

                Console.Write("Confirm your password: ");
                confirmPassword = Console.ReadLine();
                if (confirmPassword != password) Console.WriteLine("Passwords do not match.");
            }

            // Create user account
            Console.WriteLine("Valid Credentials, Please enter your information.");
            string name;
            string address;
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.Write("Enter your address: ");
            address = Console.ReadLine();
            User newUser = new User(username, password, name, address);
            users.AddUser(newUser);
            Console.WriteLine("Account created successfully!\n");
        }

    }
}