using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleClient
{
    internal class NavigationPanel
    {
        delegate void method();
        public static void MainMenu()
        {
            string[] items = {
                "User Panel",
                "Admin Panel",
                "Collections Panel",
                "Feeds Panel",
                "Exit" };

            method[] methods = new method[] {
                UserPanel,
                AdminPanel,
                CollectionsPanel,
                FeedsPanel,
                Exit };

            ConsoleMenu menu = new ConsoleMenu(items);
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                methods[menuResult]();
            } while (menuResult != items.Length - 1);
        }

        private static void UserPanel()
        {
            string[] items = {
                "Regster",
                "Authenticate",
                "Get my info",
                "Сhange my data",
                "Back" };

            method[] methods = new method[] {
                UserActions.Register,
                UserActions.Authenticate,
                UserActions.GetUserInfo,
                UserActions.UpdateUser,
                Exit };

            NavigateMenu(items, methods);
        }

        private static void AdminPanel()
        {
            string[] items = {
                "Regiser new admin",
                "Get all users",
                "Get user details",
                "Delete user",
                "Get server info",
                "Back" };

            method[] methods = new method[] {
                AdminActions.RegisterAdmin,
                AdminActions.GetAllUsers,
                AdminActions.GetUserById,
                AdminActions.DeleteUser,
                AdminActions.GetServerInfo,
                Exit};

            NavigateMenu(items, methods);
        }

        private static void CollectionsPanel()
        {
            string[] items = {
                "Create new nollection",
                "Get all collections",
                "Get collection info by Id",
                "Update collection",
                "Delete collection",
                "Back" };

            method[] methods = new method[] {
                CollectionActions.CreateCollection,
                CollectionActions.GetAllCollections,
                CollectionActions.GetCollectionById,
                CollectionActions.UpdateCollection,
                CollectionActions.DeleteCollection,
                Exit };

            NavigateMenu(items, methods);
        }

        private static void FeedsPanel()
        {
            string[] items = {
                "Add feed to collection",
                "Get all feeds from collection",
                "Get feed info by id",
                "Update feed in collection",
                "Remove feed from collection",
                "Read all feeds from collection",
                "Read feed by id",
                "Read feed by url",
                "Back" };

            method[] methods = new method[] {
                FeedActions.AddFeedToCollection,
                FeedActions.GetAllFeedsFromCollection,
                FeedActions.GetFeedById,
                FeedActions.UpdateFeedInCollection,
                FeedActions.RemoveFeedFromCollection,
                FeedActions.ReadAllFeedsFromCollection,
                FeedActions.ReadFeedById,
                FeedActions.ReadFeedByUrl,
                Exit };

            NavigateMenu(items, methods);
        }
               
        private static void Exit()
        {
        }

        private static void NavigateMenu(string[] items, method[] methods)
        {
            ConsoleMenu menu = new ConsoleMenu(items);
            int menuResult;
            do
            {
                menuResult = menu.PrintMenu();
                if (menuResult == items.Length - 1) continue;
                Console.WriteLine();
                methods[menuResult]();
                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            } while (menuResult != items.Length - 1);
        }
    }
}
