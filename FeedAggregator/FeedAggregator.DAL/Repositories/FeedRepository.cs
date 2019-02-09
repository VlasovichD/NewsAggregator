using FeedAggregator.DAL.EF;
using FeedAggregator.DAL.Entities;
using FeedAggregator.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedAggregator.DAL.Repositories
{
    public class FeedRepository : IRepository<Feed>
    {
        private DataContext context;

        public FeedRepository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<Feed> GetAll()
        {
            return context.Feeds.ToList();
        }

        public IEnumerable<Feed> GetAll(Func<Feed, bool> predicate)
        {
            return context.Feeds.Where(predicate).ToList();
        }

        public Feed GetById(int feedId)
        {
            return context.Feeds.Find(feedId);
        }

        public void Create(Feed feed)
        {
            context.Feeds.Add(feed);
        }

        public void Update(Feed feed)
        {
            context.Entry(feed).State = EntityState.Modified;
        }

        public void Delete(int feedId)
        {
            Feed feed = context.Feeds.Find(feedId);
            if (feed != null)
                context.Feeds.Remove(feed);
        }


    }
}
