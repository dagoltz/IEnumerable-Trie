# IEnumerable-Trie
C# implementation of a Trie (prefix tree) that is IEnumerable. You may conduct a prefix tree and not have to run on the trie all the way to it's end - just for the amount of results you need. This is perfect for autocomplete or any other scenario where you may want a fixed amount of results yielded in every search.

## Usage
You can go ahead and use IEnumerableTrie.Trie. all you need to do is implement "IHasStringKeys" in the class you want to save in the Trie. example:
```C#
public class Stock : IHasStringKeys
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string LastSale { get; set; }
        public string MarketCap { get; set; }

        public string[] Keys { get { return new[] {Symbol.ToLower(), Name.ToLower()}; } }

        public override string ToString()
        {
            return String.Format("{0} ({1}). Last Sale: {2}, MarketCap: {3}", Name, Symbol, LastSale, MarketCap);
        }
    }
```
Once you did, you can create your Trie:
```C#
IEnumerable<Stock> stocks = LoadStocks("NasdaqCompanyList.csv");
Trie<Stock> trie = new Trie<Stock>(stocks);

// Search for a stock by prefix. as this is Ienumrable, we don't iterate over the Trie yet:
IEnumerable<Stock> results = trie.GetValuesByPrefix("micro");
// we only want the first 100, and will only iterate over the Trie until we find 100 max
foreach(var stock in results.Take(10))
{
    Console.WriteLine(stock);
}

```
There are a few Simple implementations ready to be used:
### KeyValueTrie
A Trie that uses the key as an idex and store it's value. The key must be a string and the value could be of any type.

### StringTrie
A Trie to store and search for strings.
