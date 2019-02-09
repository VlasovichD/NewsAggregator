using ConsoleClient.Models;
using System;

namespace ConsoleClient
{
    internal class AdminActions : BaseActions
    {
        // Registration of Admin
        public static void RegisterAdmin()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Regstration of Admin\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            UserModel user = GetRegUserData();

            try
            {
                var response = AdminRequests.RegisterAdmin(Token, user);
                Console.WriteLine($"Ok\nID = {response}");
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // Getting all users
        public static void GetAllUsers()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting All Users\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TryGetUsersInfo();
        }

        // Getting User by Id
        public static void GetUserById()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting User by Id\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetUsersInfo())
            {

                Console.WriteLine("\nChoose user id:");
                int.TryParse(Console.ReadLine(), out int userId);

                try
                {
                    var user = AdminRequests.GetUserById(Token, userId);

                    Console.WriteLine($"\nId: {user.Id}");
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
        }

        // Deleting user
        public static void DeleteUser()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Deleting of User\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetUsersInfo())
            {
                Console.WriteLine("\nChoose users id to delete:");
                int.TryParse(Console.ReadLine(), out int userId);

                try
                {
                    var response = AdminRequests.DeleteUser(Token, userId);
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

        // Geting server info 
        public static void GetServerInfo()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Server Info\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            try
            {
                var response = AdminRequests.GetServerInfo(Token);
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
