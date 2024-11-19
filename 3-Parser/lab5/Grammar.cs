using System.Text.Json;

namespace FLCD._3_Parser.lab5
{
    public class Grammar
    {
        public GrammarData GrammarData { get; set; }

        public Grammar()
        {
            GrammarData = new GrammarData();
        }

        public Grammar(GrammarData grammarData)
        {
            GrammarData = grammarData;
        }

        public static Grammar LoadFromJson(string filePath)
        {
            string jsonString = File.ReadAllText(filePath);
            GrammarData? grammarData = JsonSerializer.Deserialize<GrammarData>(jsonString);

            if (grammarData == null)
            {
                return new Grammar();
            }

            return new Grammar(grammarData);
        }

        public void DisplayGrammarData()
        {
            Console.WriteLine("\nStart Symbol: " + GrammarData.StartSymbol);
            Console.WriteLine("\nNon Terminals: " + string.Join(", ", GrammarData.NonTerminals));
            Console.WriteLine("\nTerminals: " + string.Join(", ", GrammarData.Terminals));
            Console.WriteLine("\nProductions:");
            foreach (var production in GrammarData.Productions)
            {
                Console.WriteLine($"{production.Key} -> {string.Join(" | ", production.Value)}");
            }
        }

        public string GetProduction(string nonTerminal)
        {
            if (!GrammarData.Productions.ContainsKey(nonTerminal))
            {
                return "Non terminal not found.";
            }

            return string.Join(" | ", GrammarData.Productions[nonTerminal]);
        }

        public bool IsCFG(bool parseByWords = false)
        {
            foreach (var production in GrammarData.Productions)
            {
                if (string.IsNullOrWhiteSpace(production.Key) || !GrammarData.NonTerminals.Contains(production.Key))
                {
                    return false;
                }

                foreach (string rule in production.Value)
                {
                    IEnumerable<string> symbols = parseByWords
                     ? rule.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                     : rule.Select(c => c.ToString());

                    foreach (string symbol in symbols)
                    {
                        if (symbol == "ε")
                        {
                            continue;
                        }

                        if (!GrammarData.NonTerminals.Contains(symbol) && !GrammarData.Terminals.Contains(symbol))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }
    }
}