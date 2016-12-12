using System.Collections.Generic;

namespace IEnumerableTrie
{
    public interface ITrie<T> : IEnumerable<T> where T : class, IHasStringKeys
    {
        void Add(T value);
        void AddRange(IEnumerable<T> value);
        void Remove(T value);

        IEnumerable<T> GetValuesByPrefix(string prefix);
        IEnumerable<T> GetValuesByExactKey(string key);
    }
}