namespace FLCD._1_Mini_Language_And_Scanner.lab2
{
    public class SymbolTable
    {
        private SymbolEntry[] _entries;
        private int _size;

        public SymbolTable(int size)
        {
            _size = size;
            _entries = new SymbolEntry[size];
        }

        public void Insert(string identifier, object value, SymbolType type)
        {
            int index = GetHash(identifier);
            SymbolEntry entry = new SymbolEntry(identifier, value, type);

            _entries[index] = entry;
        }

        public SymbolEntry? GetEntry(string identifier)
        {
            int index = GetHash(identifier);
            var entry = _entries[index];

            if (entry != null && entry.Identifier == identifier)
            {
                return entry;
            }

            return null;
        }

        private int GetHash(string identifier)
        {
            return Math.Abs(identifier.GetHashCode()) % _size;
        }
    }
}
