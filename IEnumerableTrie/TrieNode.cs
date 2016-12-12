using System.Collections.Generic;

namespace IEnumerableTrie
{
    internal class TrieNode<T> where T : class, IHasStringKeys
    {
        public IDictionary<char, TrieNode<T>>  Edges = new Dictionary<char, TrieNode<T>>();
        public TrieNode<T> Parent { get; set; }
        public char UpEdge { get; set; }
        public List<T> Values { get; set; }
    }
}