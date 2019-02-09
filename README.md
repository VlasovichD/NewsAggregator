# NewsAggregator
NewsAggregator consists of .Net Core API FeedAggregator and ConsoleClient.

FeedAggregator is a .net API used for managing of collections of feeds created by each user registered on it.
It use JWT Bearer authorization based on Tokens with Roles(Admin, User).
FeedAggregator can read and parse RSS and ATOM feeds and send them to client in JSON format.

ConsoleClient allows to users register, authenticate and manage account.
There is particular Panel for Admins.
Each user can Create, Read, Update and Delete self Collections of feeds and Feeds in it.
Also user can see the content in each feed.
