using ConsoleClient.Models;
using System;

namespace ConsoleClient
{
    internal class FeedActions : BaseActions
    {     
        // Adding feed to collection
        public static void AddFeedToCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Adding Feed to Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Feed Name:");
            string name = Console.ReadLine();

            Console.WriteLine("Enter Feed Description:");
            string description = Console.ReadLine();

            Console.WriteLine("Enter Feed Url:");
            string url = Console.ReadLine();

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id to update:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                FeedModel feed = new FeedModel
                {
                    Name = name,
                    Description = description,
                    Url = url,
                    CollectionId = collectionId
                };

                try
                {
                    var response = FeedRequests.AddFeedToCollection(Token, feed);
                    Console.WriteLine($"Ok\nID = {response}");
                }
                catch (AppException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Getting all feeds in collection
        public static void GetAllFeedsFromCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting All Feeds in Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                TryGetFeedsInfo(collectionId);
            }
        }

        // Getting feed info
        public static void GetFeedById()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Getting Feed Info\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                if (TryGetFeedsInfo(collectionId))
                {
                    Console.WriteLine("\nChoose feed id:");
                    int.TryParse(Console.ReadLine(), out int feedId);

                    try
                    {
                        var feed = FeedRequests.GetFeedById(Token, collectionId, feedId);

                        Console.WriteLine($"\nId: {feed.Id}");
                        Console.WriteLine($"Name: {feed.Name}");
                        Console.WriteLine($"Description: {feed.Description}");
                        Console.WriteLine($"Url: {feed.Url}");
                        Console.WriteLine($"Collection Id: {feed.CollectionId}");
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

        // Updating feed in collection
        public static void UpdateFeedInCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Updating of Feed in Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                if (TryGetFeedsInfo(collectionId))
                {
                    Console.WriteLine("\nChoose feed id to update:");
                    int.TryParse(Console.ReadLine(), out int feedId);

                    Console.WriteLine("\nEnter new Feed Name:");
                    string name = Console.ReadLine();

                    Console.WriteLine("Enter new Feed Description:");
                    string description = Console.ReadLine();

                    Console.WriteLine("Enter new Feed Url:");
                    string url = Console.ReadLine();

                    FeedModel feed = new FeedModel
                    {
                        Id = feedId,
                        Name = name,
                        Description = description,
                        Url = url,
                        CollectionId = collectionId
                    };

                    try
                    {
                        var response = FeedRequests.UpdateFeedInCollection(Token, feed);
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

        // Removing feed from collection
        public static void RemoveFeedFromCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Removing of Feed from Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {

                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                if (TryGetFeedsInfo(collectionId))
                {

                    Console.WriteLine("\nChoose feed id to delete:");
                    int.TryParse(Console.ReadLine(), out int feedId);

                    try
                    {
                        var response = FeedRequests.RemoveFeedFromCollection(Token, collectionId, feedId);
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

        // Reading all feeds from collection
        public static void ReadAllFeedsFromCollection()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Reading All Feeds From Collection\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                try
                {
                    var feeds = FeedRequests.ReadAllFeedsFromCollection(Token, collectionId);

                    int counter = 1;
                    foreach (var feed in feeds)
                    {
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine($"\n Feed # {counter++}");
                        Console.BackgroundColor = ConsoleColor.Black;
                        Console.ForegroundColor = ConsoleColor.White;

                        ShowFeedContent(feed);
                    }
                }
                catch (AppException e)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(e.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }

        // Reading feed by id
        public static void ReadFeedById()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Reading Feed by Id\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            if (TryGetCollectionsInfo())
            {
                Console.WriteLine("\nChoose collection id:");
                int.TryParse(Console.ReadLine(), out int collectionId);

                if (TryGetFeedsInfo(collectionId))
                {

                    Console.WriteLine("\nChoose feed id:");
                    int.TryParse(Console.ReadLine(), out int feedId);

                    try
                    {
                        var feed = FeedRequests.ReadFeedById(Token, collectionId, feedId);

                        ShowFeedContent(feed);
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

        // Reading feed by url
        public static void ReadFeedByUrl()
        {
            Console.BackgroundColor = ConsoleColor.Green;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("Reading Feed by Url\n");
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;

            Console.WriteLine("Enter Feed Url:");
            string url = Console.ReadLine();

            try
            {
                var feed = FeedRequests.ReadFeedByUrl(Token, url);

                ShowFeedContent(feed);
            }
            catch (AppException e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        private static void ShowFeedContent(FeedContentModel feed)
        {
            Console.WriteLine();
            Console.WriteLine($"Title: {feed.Title}");
            Console.WriteLine($"Description: {feed.Description}");
            Console.WriteLine($"Link: {feed.Link}");
            Console.WriteLine($"Last Updated Date: {feed.LastUpdatedDate}");
            Console.WriteLine($"Image Url: {feed.ImageUrl}");

            foreach (var item in feed.Items)
            {
                Console.WriteLine();
                Console.WriteLine($"\t Item Title: {item.Title}");
                Console.WriteLine($"\t Item Description: {item.Description}");
                Console.WriteLine($"\t Item Link: {item.Link}");
            }
        }
    }
}
