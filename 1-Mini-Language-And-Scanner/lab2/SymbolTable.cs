namespace FLCD._1_Mini_Language_And_Scanner.lab2
{
    using System.Collections.Generic;

    public class SymbolTable
    {
        private readonly List<SymbolEntry>[] _entries;

        public SymbolTable(int size)
        {
            _entries = new List<SymbolEntry>[size];
            for (int i = 0; i < size; i++) _entries[i] = new List<SymbolEntry>();
        }

        public Tuple<int, int> InsertAndGetPosition(string key, SymbolType type)
        {
            int index = GetHash(key);
            _entries[index].Add(new SymbolEntry(key, type));

            return new Tuple<int, int>(index, _entries[index].Count - 1);
        }

        public Tuple<int, int> GetPosition(string key)
        {
            int index = GetHash(key);
            for (int i = 0; i < _entries[index].Count; i++)
            {
                if (_entries[index][i].Value == key)
                {
                    return new Tuple<int, int>(index, i);
                }
            }

            return new Tuple<int, int>(-1,-1);
        }

        public SymbolEntry? GetEntry(string key)
        {
            int index = GetHash(key);
            return _entries[index].Find(e => e.Value == key);
        }

        public List<SymbolEntry>[] GetEntries() => _entries;

        private int GetHash(string key) => Math.Abs(key.GetHashCode()) % _entries.Length;
    }
}