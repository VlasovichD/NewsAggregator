using FeedAggregator.DAL.Entities;
using System;

namespace FeedAggregator.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Collection> Collections { get; }
        IRepository<Feed> Feeds { get; }
        IRepository<User> Users { get; }
        void Save();
    }
}
