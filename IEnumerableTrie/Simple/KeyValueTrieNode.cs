using System;
using System.Collections.Generic;
using System.Linq;


namespace IEnumerableTrie.Simple
{
    public class KeyValueTrieNode<TValue> : IHasStringKeys
    {
        public string[] Keys {
            get { return new[] {Key}; }
        }

        public string Key { get; private set; }
        public TValue Value { get; private set; }

        public KeyValueTrieNode(KeyValuePair<string, TValue> keyValuePair) : this(keyValuePair.Key, keyValuePair.Value)
        {}

        public KeyValueTrieNode(string key, TValue value)
        {
            Value = value;
            Key = key;
        }

        public static implicit operator KeyValueTrieNode<TValue>(KeyValuePair<string, TValue> keyValuePair)
        {
            return new KeyValueTrieNode<TValue>(keyValuePair);
        }

        public static implicit operator KeyValuePair<string, TValue>(KeyValueTrieNode<TValue> node)
        {
            return new KeyValuePair<string, TValue>(node.Key, node.Value);
        }
    }
}
