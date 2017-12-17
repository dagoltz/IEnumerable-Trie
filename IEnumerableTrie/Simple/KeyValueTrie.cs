using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableTrie.Simple
{
    public class KeyValueTrie<TValue> : Trie<KeyValueTrieNode<TValue>>
    {
        public KeyValueTrie()
        {}

        public KeyValueTrie(IEnumerable<KeyValueTrieNode<TValue>> values) : base(values)
        {}
    }
}
