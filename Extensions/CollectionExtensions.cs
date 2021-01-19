using System;
using System.Collections.Generic;
using System.Linq;

namespace Orbital.Core
{
    public static class CollectionExtensions
    {
        public static IEnumerable<T> TopologicalSort<T>(this IEnumerable<T> source, Func<T, IEnumerable<T>> dependencySelector)
        {
            var resultList = new List<T>();
            var visitedItems = new HashSet<T>();

            var sourceItems = source as T[] ?? source.ToArray();
            foreach (var sourceItem in sourceItems)
            {
                void Visit(T item)
                {
                    if (!visitedItems.Contains(item))
                    {
                        visitedItems.Add(item);

                        var dependencies = dependencySelector?.Invoke(item)?.ToArray();
                        if (dependencies != null && dependencies.Any())
                        {
                            foreach (var dependency in dependencies)
                            {
                                if (!sourceItems.Contains(dependency))
                                {
                                    throw new Exception(
                                        $"[{nameof(dependencySelector)}()] function returns a dependency [{dependency}] " +
                                        $"which is not in the [{nameof(sourceItems)}] collection"
                                    );
                                }

                                Visit(dependency);
                            }
                        }

                        resultList.Add(item);
                    }
                    else
                    {
                        if (!resultList.Contains(item))
                        {
                            throw new Exception("Cyclic dependency detected!");
                        }
                    }
                }
                Visit(sourceItem);
            }

            return resultList;
        }
    }
}