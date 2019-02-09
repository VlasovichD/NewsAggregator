using ConsoleClient.Models;
using System;

namespace ConsoleClient
{
    internal class CollectionActions : BaseActions
    {
        // Creating collection
        public static void CreateCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Creation of Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Collection Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Collection Description:");
            string description = Console.ReadLine();

            CollectionModel collection = new CollectionModel
            {
                Name = name,
                Description = description
            };

            try
            {
                var response = CollectionRequests.CreateCollection(Token, collection);
                Console.WriteLine($"Ok\nID = {response}");
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        // Getting all collections
        public static void GetAllCollections()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting All Collections\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            TryGetCollectionsInfo();
        }

        // Getting Collection by Id
        public static void GetCollectionById()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting Collection by Id\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                try
                {
                    var collection = CollectionRequests.GetCollectionById(Token, collectionId);

                    Console.WriteLine($"\nId: {collection.Id}");
                    Console.WriteLine($"Name: {collection.Name}");
                    Console.WriteLine($"Description: {collection.Description}");
                }
                catch (AppException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Updating collection
        public static void UpdateCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Updating of Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id to update:");
                int.TryParse(Console.ReadLine(), out int id);

                Console.WriteLine("Enter new Collection Name:");
                string name = Console.ReadLine();

                Console.WriteLine("Enter new Collection Description:");
                string description = Console.ReadLine();

                CollectionModel collection = new CollectionModel
                {
                    Id = id,
                    Name = name,
                    Description = description
                };

                try
                {
                    var response = CollectionRequests.UpdateCollection(Token, collection);
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

        // Deleting collection
        public static void DeleteCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Deleting of Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id to delete:");
                int.TryParse(Console.ReadLine(), out int id);

                try
                {
                    var response = CollectionRequests.DeleteCollection(Token, id);
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
}
