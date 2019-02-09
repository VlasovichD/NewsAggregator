using FeedAggregator.DAL.EF;
using FeedAggregator.DAL.Entities;
using FeedAggregator.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeedAggregator.DAL.Repositories
{
    public class CollectionRepository : IRepository<Collection>
    {
        private DataContext context;

        public CollectionRepository(DataContext context)
        {
            this.context = context;
        }
        public IEnumerable<Collection> GetAll()
        {
            return context.Collections.ToList();
        }

        public IEnumerable<Collection> GetAll(Func<Collection, bool> predicate)
        {
            return context.Collections.Where(predicate).ToList();
        }

        public Collection GetById(int collectionId)
        {
            return context.Collections.Find(collectionId);
        }

        public void Create(Collection collection)
        {
            context.Collections.Add(collection);
        }

        public void Update(Collection collection)
        {
            context.Entry(collection).State = EntityState.Modified;
        }

        public void Delete(int collectionId)
        {
            Collection collection = context.Collections.Find(collectionId);
            if (collection != null)
                context.Collections.Remove(collection);
        }
    }
}
