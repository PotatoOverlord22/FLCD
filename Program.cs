﻿using FLCD._1_Mini_Language_And_Scanner.lab2;
using FLCD._1_Mini_Language_And_Scanner.lab3;

namespace FLCD
{
    public class Program
    {
        static void Main(string[] args)
        {
            Scanner scanner = new Scanner(new SymbolTable(100), "../../../1-Mini-Language-And-Scanner/lab1b/token.in");
            scanner.ScanSourceFile("../../../1-Mini-Language-And-Scanner/lab1a/p1", "../../../1-Mini-Language-And-Scanner/lab2/pif.out", 
                "../../../1-Mini-Language-And-Scanner/lab2/st.out");
        }
    }
}