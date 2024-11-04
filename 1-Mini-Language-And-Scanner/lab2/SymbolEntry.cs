namespace FLCD._1_Mini_Language_And_Scanner.lab2
{
    public class SymbolEntry
    {
        public string Value { get; set; }
        public SymbolType Type { get; set; }

        public SymbolEntry(string value, SymbolType type)
        {
            Value = value;
            Type = type;
        }
    }
}
