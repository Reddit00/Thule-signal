using System;
using System.Collections.Generic;
using ThuleSignal.Domain.Common;

namespace ThuleSignal.App.Services.Contracts
{
    public interface IRepository<T> where T : class, IEntity
    {
        void Add(T entity);
        T? GetById(string id);
        IReadOnlyList<T> GetAll();
        void Remove(string id);
    }
}