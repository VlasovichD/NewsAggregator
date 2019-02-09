using System;
using System.Collections.Generic;

namespace FeedAggregator.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAll(Func<T, bool> predicate);
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
