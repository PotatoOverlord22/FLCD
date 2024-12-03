using System.Text.Json;

namespace FLCD._3_Parser.lab5
{
    public class Grammar
    {
        public GrammarData GrammarData { get; set; }
        public bool ParseByWords { get; set; }
        public const string EnrichedGrammarStartSymbol = "S'";
        public static string Epsilon = "ε";

        public Grammar(bool parseByWords = false)
        {
            GrammarData = new GrammarData();
            ParseByWords = parseByWords;
        }

        public Grammar(GrammarData grammarData, bool parseByWords = false)
        {
            GrammarData = grammarData;
            ParseByWords = parseByWords;
        }

        public static Grammar LoadFromJson(string filePath, bool parseByWords = false)
        {
            string jsonString = File.ReadAllText(filePath);
            GrammarData? grammarData = JsonSerializer.Deserialize<GrammarData>(jsonString);

            if (grammarData == null)
            {
                return new Grammar();
            }

            return new Grammar(grammarData, parseByWords);
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
                    var symbols = ParseRule(rule);

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

        public List<string> ParseRule(string rule)
        {
            return ParseByWords
                ? rule.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList()
                : rule.Select(c => c.ToString()).ToList();
        }

        public Grammar GetEnrichedGrammar()
        {
            if (GrammarData.StartSymbol == EnrichedGrammarStartSymbol)
            {
                throw new Exception("Grammar is already enriched.");
            }

            var enrichedGrammar = new Grammar(GrammarData.DeepCopy(), ParseByWords);
            enrichedGrammar.EnrichGrammar();

            return enrichedGrammar;
        }

        public void EnrichGrammar()
        {
            if (GrammarData.StartSymbol == EnrichedGrammarStartSymbol)
            {
                return;
            }

            GrammarData.NonTerminals.Add(EnrichedGrammarStartSymbol);
            GrammarData.Productions[EnrichedGrammarStartSymbol] = new HashSet<string> { GrammarData.StartSymbol };
            GrammarData.StartSymbol = EnrichedGrammarStartSymbol;
        }

        public List<(string, string)> GetOrderedProductions()
        {
            var orderedProductions = new List<(string, string)>();

            foreach (var production in GrammarData.Productions)
            {
                string nonTerminal = production.Key;

                foreach (var rule in production.Value)
                {
                    orderedProductions.Add((nonTerminal, rule));
                }
            }

            return orderedProductions;
        }

    }
}