using FLCD._3_Parser.lab5;

namespace FLCD
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../3-Parser/lab5/g1.json";
            Grammar grammar = Grammar.LoadFromJson(filePath, parseByWords: false);
            Parser parser = new Parser(grammar);

            Console.WriteLine(parser.CreateCannonicalCollection());
        }
    }
}