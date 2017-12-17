using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IEnumerableTrie.Simple
{
    public class StringTrieNode : IHasStringKeys
    {
        public string[] Keys { get; private set; }

        public StringTrieNode(string key)
        {
            Keys = new []{key};
        }

        public static implicit operator StringTrieNode(string str)
        {
            return new StringTrieNode(str);
        }

        public static implicit operator string(StringTrieNode node)
        {
            return node.Keys.First();
        }

        public override string ToString()
        {
            return this;
        }
    }
}
