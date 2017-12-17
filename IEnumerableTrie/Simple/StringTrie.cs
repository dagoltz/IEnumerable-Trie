using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableTrie.Simple
{
    public class StringTrie: Trie<StringTrieNode>
    {
        public StringTrie()
        {}

        public StringTrie(IEnumerable<StringTrieNode> values)
            : base(values)
        {}
    }
}
