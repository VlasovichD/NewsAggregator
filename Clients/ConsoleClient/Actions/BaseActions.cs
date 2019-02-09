using ConsoleClient.Models;
using System;

namespace ConsoleClient
{
    internal class BaseActions
    {
        protected static string Token { get; set; }

        // Get user data for registration
        protected static UserModel GetRegUserData()
        {
            Console.WriteLine("Enter First Name:");
            string firstName = Console.ReadLine();

            Console.WriteLine("Enter Last Name:");
            string lastName = Console.ReadLine();

            Console.WriteLine("Enter Login:");
            string userName = Console.ReadLine();

            Console.WriteLine("Enter Password:");
            string password = Console.ReadLine();

            UserModel user = new UserModel
            {
                FirstName = firstName,
                LastName = lastName,
                Username = userName,
                Password = password
            };
            return user;
        }

        protected static bool TryGetUsersInfo()
        {
            try
            {
                var users = AdminRequests.GetAllUsers(Token);

                Console.WriteLine("List of Users:");

                if (users.Count != 0)
                {
                    foreach (var item in users)
                    {
                        Console.WriteLine($"{item.Id} {item.FirstName} {item.LastName} - {item.Username} ({item.Role})");
                    }
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("List is empty");
                    Console.ForegroundColor = ConsoleColor.White;

                    return false;
                }
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        protected static bool TryGetCollectionsInfo()
        {
            try
            {
                var collections = CollectionRequests.GetAllCollections(Token);

                Console.WriteLine("List of Collections:");

                if (collections.Count != 0)
                {
                    foreach (var item in collections)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} - {item.Description}");
                    }
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("List is empty");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

        protected static bool TryGetFeedsInfo(int collectionId)
        {
            try
            {
                var feeds = FeedRequests.GetAllFeedsFromCollection(Token, collectionId);

                Console.WriteLine("\nList of Feeds in current collection:");

                if (feeds.Count != 0)
                {
                    foreach (var item in feeds)
                    {
                        Console.WriteLine($"{item.Id} {item.Name} - {item.Description} ({item.Url})");
                    }
                    return true;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("List is empty");
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                }
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }
        }

    }
}
