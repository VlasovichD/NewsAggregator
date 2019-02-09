using ConsoleClient.Models;
using System;

namespace ConsoleClient
{
    internal class UserActions : BaseActions
    {
        // Registration of user
        public static void Register()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Regstration of User\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            UserModel user = GetRegUserData();

            try
            {
                var response = UserRequests.Register(user);
                Console.WriteLine($"Ok\nID = {response}");
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // Getting token
        public static void Authenticate()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Authentication\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;


            Console.WriteLine("Enter Login:");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            try
            {
                var response = UserRequests.Authenticate(userName, password);
                Token = response["token"];
                Console.WriteLine("Ok");
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // Getting user info
        public static void GetUserInfo()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting Current User Info\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                var user = UserRequests.GetUserInfo(Token);

                Console.WriteLine($"Id: {user.Id}");
                Console.WriteLine($"First Name: {user.FirstName}");
                Console.WriteLine($"Last Name: {user.LastName}");
                Console.WriteLine($"Username: {user.Username}");
                Console.WriteLine($"Role: {user.Role}");
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // Updating user
        public static void UpdateUser()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Updating of User\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter new FirstName:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter new LastName:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter new Username:");
            string username = Console.ReadLine();

            Console.WriteLine("Enter new Password:");
            string password = Console.ReadLine();

            UserModel user = new UserModel
            {
                FirstName = firstName,
                LastName = lastName,
                Username = username,
                Password = password,
            };

            try
            {
                var response = UserRequests.UpdateUser(Token, user);
                Console.WriteLine(response);
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }
}
