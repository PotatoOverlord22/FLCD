using FLCD._1_Mini_Language_And_Scanner.lab2;
using System.Text.RegularExpressions;

namespace FLCD._1_Mini_Language_And_Scanner.lab3
{
    public class Scanner
    {
        private readonly string _tokensFilePath;
        private readonly SymbolTable _symbolTable;
        private readonly Dictionary<string, string> _tokenDefinitions;

        public Scanner(SymbolTable symbolTable, string tokensFilePath)
        {
            _symbolTable = symbolTable;
            _tokenDefinitions = new Dictionary<string, string>();
            _tokensFilePath = tokensFilePath;
            LoadTokens(_tokensFilePath);
        }

        public void LoadTokens(string tokenFilePath)
        {
            var lines = File.ReadAllLines(tokenFilePath);
            foreach (var line in lines)
            {
                var parts = line.Split(new[] { ":=" }, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2)
                {
                    var tokenName = parts[0].Trim();
                    var tokenValue = parts[1].Trim();
                    _tokenDefinitions[tokenValue] = tokenName;
                }
            }
        }

        public void ScanSourceFile(string sourceFilePath, string outputFilePath, string symbolTableOutputFilePath)
        {
            List<Tuple<string, string>> pif = new List<Tuple<string, string>>();
            var lines = File.ReadAllLines(sourceFilePath);
            int lineIndex = 1;
            bool hasErrors = false;

            foreach (var line in lines)
            {
                List<string> tokens = Tokenize(line);
                if (tokens.Count > 0 && tokens[0] == "//")
                {
                    pif.Add(new Tuple<string, string>(tokens[0], "-1"));
                    continue;
                }

                foreach (var token in tokens)
                {
                    if (_tokenDefinitions.ContainsKey(token))
                    {
                        pif.Add(new Tuple<string, string>(token, "-1"));
                    }
                    else if (IsIdentifier(token))
                    {
                        Tuple<int, int> position = _symbolTable.GetPosition(token);
                        if (position.Item1 == -1)
                        {
                            position = _symbolTable.InsertAndGetPosition(token, SymbolType.Identifier);
                        }

                        pif.Add(new Tuple<string, string>("identifier", $"({position.Item1}, {position.Item2})"));
                    }
                    else if (IsIntegerConstant(token))
                    {
                        var position = _symbolTable.GetPosition(token);
                        if (position.Item1 == -1)
                        {
                            position = _symbolTable.InsertAndGetPosition(token, SymbolType.IntConstant);
                        }

                        pif.Add(new Tuple<string, string>("const", $"({position.Item1}, {position.Item2})"));
                    }
                    else
                    {
                        Console.WriteLine($"Lexical error at line {lineIndex}: unrecognized token '{token}'");
                        hasErrors = true;
                    }
                }

                lineIndex++;
            }

            WritePif(outputFilePath, pif);
            WriteSymbolTable(symbolTableOutputFilePath);
            if (!hasErrors)
            {
                Console.WriteLine("No lexical errors found");
            }
        }

        private List<string> Tokenize(string line)
        {
            string pattern = @"\s+|(?<=\W)(?=\w)|(?<=\w)(?=\W)";

            var tokens = Regex.Split(line, pattern);

            return tokens
                .Where(t => !string.IsNullOrWhiteSpace(t)) // Remove empty tokens
                .Select(t => t.Trim()) // Trim any whitespace from tokens
                .ToList(); // Convert to List<string>
        }


        private bool IsIdentifier(string token)
        {
            return Regex.IsMatch(token, @"^[a-zA-Z_][a-zA-Z0-9_]*$");
        }

        private bool IsIntegerConstant(string token)
        {
            return int.TryParse(token, out _);
        }

        private void WritePif(string filePath, List<Tuple<string, string>> pif)
        {
            using (var writer = new StreamWriter(filePath))
            {
                foreach (var entry in pif)
                {
                    writer.WriteLine($"{entry.Item1} -> {entry.Item2}");
                }
            }
        }

        public void WriteSymbolTable(string filePath)
        {
            List<SymbolEntry>[] entries = _symbolTable.GetEntries();
            using (var writer = new StreamWriter(filePath))
            {
                for (int bucketIndex = 0; bucketIndex < entries.Length; bucketIndex++)
                {
                    List<SymbolEntry> bucket = entries[bucketIndex];
                    for (int itemIndexInBucket = 0; itemIndexInBucket < bucket.Count; itemIndexInBucket++)
                    {
                        SymbolEntry symbolEntry = bucket[itemIndexInBucket];
                        writer.WriteLine($"({bucketIndex}, {itemIndexInBucket}) -> {symbolEntry.Value}");
                    }
                }
            }
        }
    }
}