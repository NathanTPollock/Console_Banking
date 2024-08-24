using System.Net.Security;

namespace ConsoleBanking
{
    class Program
    {
        static UserList users = new UserList();
        static User? currentUser;
        static Account? currentAccount;
        static void Main(string[] args)
        {
            try
            {
                // Start thread that will accumulate interest on all open accounts every 1 minute.
                Thread interestThread = new Thread(() =>
                {
                    while (true)
                    {                        
                        Parallel.ForEach(users.GetAllUsers(), user =>
                        {
                            Parallel.ForEach(user.GetAllAccounts(), account =>
                            {
                                if (account.GetAccountType() == "Savings")
                                {
                                    SavingsAccount savingsAccount = (SavingsAccount)account;
                                    savingsAccount.CompoundInterest();
                                }
                            });
                        });
                        Thread.Sleep(60000);
                    }
                });
                interestThread.IsBackground = true;
                interestThread.Start();

                // Start thread that will charge account fees on all open accounts every 5 minutes.
                Thread feeThread = new Thread(() =>
                {
                    while (true)
                    {
                        Parallel.ForEach(users.GetAllUsers(), user =>
                        {
                            Parallel.ForEach(user.GetAllAccounts(), account =>
                            {
                                account.ChargeFee();
                            });
                        });
                        Thread.Sleep(300000);
                    }
                });
                feeThread.IsBackground = true;
                feeThread.Start();
                // Send user to main page to login or create account
                bool loggedIn;
                bool exit = false;
                while (!exit)
                {
                    loggedIn = MainPage();
                    if (!loggedIn) return;

                    // User is logged in, go to their page

                    UserPage();
                    Console.Clear();
                    loggedIn = false;
                }
                
            }
            catch(Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
        }

        /// <summary>
        /// Main page for Console Banking
        /// </summary>
        /// <returns>True if user successfully logged in. Otherwise false.</returns>
        static bool MainPage()
        {
            string? input;
            bool exit = false;
            bool loggedIn = false;
            while (!exit && !loggedIn)
            {
                // Welcome User, Prompt for login or create account
                Console.Write("Welcome to Console Banking! Would you like to login or create an account? (login/create/exit): ");
                input = Console.ReadLine();
                input = input?.ToLower(); // Convert to lowercase if input not null
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

        /// <summary>
        /// Current user's page to console
        /// </summary>
        /// <exception cref="Exception"></exception>
        static void UserPage()
        {
            try
            {
                if (currentUser == null) throw new Exception("currentUser is null.");
                Console.Clear();
                string? input;
                bool exit = false;
                while (!exit)
                {
                    // Display user information
                    Console.WriteLine(currentUser.ToString());

                    // Prompt for command
                    Console.Write("\nEnter a command (add account, edit account, remove account, log out): ");
                    input = Console.ReadLine();
                    input = input?.ToLower();
                    switch (input)
                    {
                        case "add account":
                            Console.Write("Enter account type (checking/savings): ");
                            input = Console.ReadLine();
                            if(input == "checking")
                            {
                                currentUser.AddAccount(new CheckingAccount());
                            }
                            else if(input == "savings")
                            {
                                currentUser.AddAccount(new SavingsAccount());
                            }
                            else
                            {
                                Console.WriteLine("Invalid account type.");
                            }
                            break;
                        case "remove account":
                            Console.Write("Enter the account number to remove: ");
                            input = Console.ReadLine();
                            if (input == null) break;
                            if (currentUser.RemoveAccount(int.Parse(input))) Console.WriteLine("Account removed successfully!");
                            else Console.WriteLine("Account not found.");
                            break;
                        case "edit account":
                            Console.Write("Enter the account number to edit: ");
                            input = Console.ReadLine();
                            if (input == null) break;
                            currentAccount = currentUser.GetAccount(int.Parse(input));
                            if (currentAccount == null)
                            {
                                Console.WriteLine("Account not found.");
                                break;
                            }
                            AccountPage();
                            break;
                        case "log out":
                            Console.WriteLine($"Logging out...\nGoodbye, {currentUser.GetName()}");
                            exit = true;
                            break;
                        default:
                            Console.WriteLine(input + " is an invalid command. Please try again.");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred: " + e.Message);
            }
            
        }
        /// <summary>
        /// Console interaction for viewing and editing an account
        /// </summary>
        /// <exception cref="Exception"></exception>
        static void AccountPage()
        {
            Console.Clear();
            if (currentAccount == null) throw new Exception("currentAccount is null.");
            Console.WriteLine("Editing Account:\n" + currentAccount.ToString());
            string? input;
            bool exit = false;
            while (!exit)
            {
                Console.Write("Enter a command (deposit, withdraw, exit): ");
                input = Console.ReadLine();
                input = input == null ? null : input.ToLower();
                switch (input)
                {
                    case "deposit":
                        Console.Write("Enter the amount to deposit: ");
                        input = Console.ReadLine();
                        if (input == null) break;
                        currentAccount.DepositFunds(decimal.Parse(input));
                        Console.WriteLine("Deposit successful!");
                        break;
                    case "withdraw":
                        Console.Write("Enter the amount to withdraw: ");
                        input = Console.ReadLine();
                        if (input == null) break;
                        currentAccount.WithdrawFunds(decimal.Parse(input));
                        Console.WriteLine("Withdrawal successful!");
                        break;
                    case "exit":
                        Console.WriteLine("Returning to overview...");
                        exit = true;
                        break;
                    default:
                        Console.WriteLine(input + " is an invalid command. Please try again.");
                        break;
                }
            }
        }

        /// <summary>
        /// Console interaction for user login page
        /// </summary>
        /// <returns>False if incorrect password or user not found. True if login successful</returns>
        static bool UserLogin()
        {
            string? username;
            string? password;
            
            // Get username and password
            Console.Write("Enter your username: ");
            username = Console.ReadLine();
            if (username == null) return false;
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

        /// <summary>
        /// Console interaction to create a new user account
        /// </summary>
        static void CreateUser()
        {
            string? username = "default";
            string? password = "default";
            string? confirmPassword = "def";
            bool userExists = true;

            // Ensure username is unique
            while (userExists)
            {
                Console.Write("Enter your desired username: ");
                username = Console.ReadLine();
                if (username == null) return;

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
            string? name;
            string? address;
            Console.Write("Enter your name: ");
            name = Console.ReadLine();
            Console.Write("Enter your address: ");
            address = Console.ReadLine();
            if (password == null || name == null || address == null) return;
            User newUser = new User(username, password, name, address);
            users.AddUser(newUser);
            Console.WriteLine("Account created successfully!\n");
        }

    }
}