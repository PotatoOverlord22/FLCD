namespace FLCD._1_Mini_Language_And_Scanner.lab2
{
    public class SymbolEntry
    {
        public SymbolType Type { get; set; }
        public string Identifier { get; set; }
        public object Value { get; set; }

        public SymbolEntry(string identifier, object value, SymbolType type)
        {
            Identifier = identifier;
            Value = value;
            Type = type;
        }
    }
}
