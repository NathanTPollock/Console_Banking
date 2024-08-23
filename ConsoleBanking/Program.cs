namespace ConsoleBanking
{
    class Program
    {
        static List<User> users = new List<User>();
        static void Main(string[] args)
        {
            // Send user to main page to login or create account
            bool loggedIn;
            loggedIn = MainPage();
            if(!loggedIn) return;
            // User is logged in

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
                        Console.WriteLine("Directing to login page...");
                        loggedIn = UserLogin();
                        break;
                    case "create":
                        // Create Account
                        Console.WriteLine("Directing to create account page...");
                        CreateUser();
                        break;
                    case "exit":
                        Console.WriteLine("Exiting Console Banking. Goodbye!");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine(input + " is an invalid command. Please try again.");
                        break;
                }
            }
            return loggedIn;
        }

        static bool UserLogin()
        {
            string username;
            string password;
            Console.Write("Enter your username: ");
            username = Console.ReadLine();
            Console.Write("Enter your password: ");
            password = Console.ReadLine();
            // Check if username and password are correct
            if(username == "admin" && password == "password")
            {
                Console.WriteLine("Login successful!");
                return true;
            }
            else
            {
                Console.WriteLine("Login failed. Please try again.");
            }
            return false;
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
                userExists = CheckUsersFor(username);
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
            users.Add(newUser);
            Console.WriteLine("Account created successfully!");
        }

        static bool CheckUsersFor(string username)
        {
            // Check if username already exists
            foreach (User user in users)
            {
                if (user.GetUsername() == username)
                {
                    Console.WriteLine("Username already exists. Please try again.");
                    return true;
                }
            }
            return false;
        } 

    }
}