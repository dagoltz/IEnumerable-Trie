using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using IEnumerableTrie.Simple;

namespace IEnumerableTrie.Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var stocks = LoadStocks("NasdaqCompanyList.csv");
            var trie = new Trie<Stock>(stocks);

            bool flag = true;
            while (flag)
            {
                System.Console.WriteLine("Please enter a prefix to search NASAQ companies or exit to finish");
                var input = System.Console.ReadLine();
                if (input == "exit")
                    flag = false;
                else if(!string.IsNullOrEmpty(input))
                {
                    var results = trie.GetValuesByPrefix(input.ToLower());
                    System.Console.Clear();
                    System.Console.WriteLine("Results for prefix: \"{0}\"",input);
                    int i = 0;
                    foreach (var result in results)
                    {
                        System.Console.WriteLine(result);
                        i++;
                    }
                    System.Console.WriteLine();
                    System.Console.WriteLine("Total: {0} results", i);
                    System.Console.WriteLine("--------");
                }
            }
        }

        private static IEnumerable<Stock> LoadStocks(string path)
        {
            using (TextReader reader = File.OpenText(path))
            {
                var csvReader = new CsvReader(reader);
                return csvReader.GetRecords<Stock>().ToList();
            }
        }
    }

    internal class Stock : IHasStringKeys
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
}
