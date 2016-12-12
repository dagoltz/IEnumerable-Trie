using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableTrie
{
    public class Trie<T> : ITrie<T> where T : class, IHasStringKeys
    {
        private readonly TrieNode<T> _root = new TrieNode<T>();

        public Trie()
        {
        }
        public Trie(IEnumerable<T> values)
        {
            AddRange(values);
        }

        public void Add(T value)
        {
            var keys = value.Keys;
            bool inserted = false;
            foreach (var key in keys)
            {
                var node = _root;
                for (int i = 0; i < key.Length; i++)
                {
                    TrieNode<T> next;
                    node.Edges.TryGetValue(key[i], out next);
                    if (next == null)
                    {
                        next = new TrieNode<T>()
                        {
                            Parent = node,
                            UpEdge = key[i]
                        };
                        if (i == key.Length - 1)
                        {
                            next.Values = new List<T>() { value };
                            inserted = true;
                        }
                        node.Edges.Add(key[i], next);
                    }
                    node = next;
                }
                if (!inserted)
                {
                    if (node.Values == null)
                    {
                        node.Values = new List<T>();
                    }
                    node.Values.Add(value);
                }
            }
        }
        public void AddRange(IEnumerable<T> values)
        {
            foreach (var value in values)
            {
                Add(value);
            }
        }
        public void Remove(T value)
        {
            var keys = value.Keys;
            foreach (var key in keys)
            {
                var node = NodeAtPrefix(key);
                if (node == null)
                {
                    return;
                }
                if (node.Values != null)
                {
                    node.Values.RemoveAll(v=> v.Equals(value));
                }
                var descendents = GetValuesRecursively(node);
                // if this is the last value in the subtree: remove this node
                if (!descendents.Any())
                {
                    node.Parent.Edges.Remove(node.UpEdge);
                }
            }
        }

        private IEnumerable<T> GetValuesRecursively(TrieNode<T> node)
        {
            if (node.Values != null)
            {
                foreach (var value in node.Values)
                {
                    yield return value;
                }
            }
            foreach (var range in node.Edges.Values.SelectMany(GetValuesRecursively))
            {
                yield return range;
            }
        }

        private TrieNode<T> NodeAtPrefix(string prefix)
        {
            var node = _root;
            foreach (var c in prefix)
            {
                TrieNode<T> next;
                node.Edges.TryGetValue(c, out next);
                if (next == null)
                {
                    return null;
                }
                node = next;
            }
            return node;
        }

        public IEnumerable<T> GetValuesByPrefix(string prefix)
        {
            var node = NodeAtPrefix(prefix);
            if (node == null)
            {
                return Enumerable.Empty<T>();
            }
            return GetValuesRecursively(node);
        }

        public IEnumerable<T> GetValuesByExactKey(string key)
        {
            var node = NodeAtPrefix(key);
            if (node == null || node.Values == null)
            {
                return Enumerable.Empty<T>();
            }
            return node.Values;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return GetValuesRecursively(_root).GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
