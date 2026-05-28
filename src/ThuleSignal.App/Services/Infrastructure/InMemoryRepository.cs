using System;
using System.Collections.Generic;
using ThuleSignal.App.Services.Contracts;
using ThuleSignal.Domain.Common;

namespace ThuleSignal.App.Services.Infrastructure
{
    public class InMemoryRepository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly Dictionary<string, T> _storage = new();

        public void Add(T entity)
        {
            if (entity == null) throw new ArgumentNullException(nameof(entity));
            
            if (_storage.ContainsKey(entity.Id))
                throw new InvalidOperationException($"Об'єкт з ID '{entity.Id}' вже існує в сховищі.");
                
            _storage[entity.Id] = entity;
            Console.WriteLine($"[Repository<{typeof(T).Name}>] Додано новий елемент з ID: {entity.Id}");
        }

        public T? GetById(string id)
        {
            return _storage.TryGetValue(id, out var entity) ? entity : null;
        }

        public IReadOnlyList<T> GetAll()
        {
            var list = new List<T>(_storage.Values);
            return list.AsReadOnly();
        }

        public void Remove(string id)
        {
            if (_storage.Remove(id))
            {
                Console.WriteLine($"[Repository<{typeof(T).Name}>] Видалено елемент з ID: {id}");
            }
        }
    }
}