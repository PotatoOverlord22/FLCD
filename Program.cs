using FLCD._1_Mini_Language_And_Scanner.lab2;
using FLCD._1_Mini_Language_And_Scanner.lab3;
using FLCD._3_Parser.lab5;

namespace FLCD
{
    public class Program
    {
        static void Main(string[] args)
        {
            string filePath = "../../../3-Parser/lab5/g1.json";
            Grammar grammar = Grammar.LoadFromJson(filePath);

            grammar.DisplayGrammarData();

            if (grammar.CFG())
            {
                Console.WriteLine("\nThe grammar is CFG.");
            }
            else
            {
                Console.WriteLine("\nThe grammar is not CFG.");
            }

            while (true)
            {
                Console.WriteLine("\nEnter a non terminal to check production");

                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }

                Console.WriteLine(grammar.GetProduction(input));
            }
        }
    }
}