using FeedAggregator.DAL.EF;
using FeedAggregator.DAL.Entities;
using FeedAggregator.DAL.Interfaces;
using System;

namespace FeedAggregator.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext context;
        private CollectionRepository collectionsRepository;
        private FeedRepository feedsRepository;
        private UserRepository usersRepository;

        public UnitOfWork(DataContext context)
        {
            this.context = context;
        }

        public IRepository<Collection> Collections
        {
            get
            {
                if (collectionsRepository == null)
                    collectionsRepository = new CollectionRepository(context);
                return collectionsRepository;
            }
        }

        public IRepository<Feed> Feeds
        {
            get
            {
                if (feedsRepository == null)
                    feedsRepository = new FeedRepository(context);
                return feedsRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (usersRepository == null)
                    usersRepository = new UserRepository(context);
                return usersRepository;
            }
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
