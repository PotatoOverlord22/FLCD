using System.Text.Json.Serialization;

namespace FLCD._3_Parser.lab5
{
    public class GrammarData
    {
        [JsonPropertyName("startSymbol")]
        public string StartSymbol { get; set; }

        [JsonPropertyName("nonTerminals")]
        public HashSet<string> NonTerminals { get; set; }

        [JsonPropertyName("terminals")]
        public HashSet<string> Terminals { get; set; }

        [JsonPropertyName("productions")]
        public Dictionary<string, HashSet<string>> Productions { get; set; }

        public GrammarData()
        {
            NonTerminals = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            Terminals = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
        }
    }
}