using System;
using System.Collections.Generic;

namespace ThuleSignal.App.Common
{
    public static class GenericDataProcessor
    {
        public static void ForEach<T>(IEnumerable<T> collection, Action<T> action)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (action == null) throw new ArgumentNullException(nameof(action));

            foreach (var item in collection)
            {
                action(item); 
            }
        }

        public static IEnumerable<TResult> Map<T, TResult>(IEnumerable<T> collection, Func<T, TResult> transform)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (transform == null) throw new ArgumentNullException(nameof(transform));

            var result = new List<TResult>();
            foreach (var item in collection)
            {
                result.Add(transform(item));
            }
            return result;
        }

        public static TAccumulate Reduce<T, TAccumulate>(IEnumerable<T> collection, TAccumulate seed, Func<TAccumulate, T, TAccumulate> accumulator)
        {
            if (collection == null) throw new ArgumentNullException(nameof(collection));
            if (accumulator == null) throw new ArgumentNullException(nameof(accumulator));

            TAccumulate currentAccumulator = seed;
            foreach (var item in collection)
            {
                currentAccumulator = accumulator(currentAccumulator, item);
            }
            return currentAccumulator;
        }
    }
}