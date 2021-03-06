﻿using System;
using System.IO;

namespace Mateer.ChangeNames.Refactored
{
    class ReadFile
    {
        static void Main()
        {
            var fileTextLines = File.ReadAllLines(@"..\..\quotes.csv");
            foreach (var line in fileTextLines)
            {
                WriteToConsoleInAppropriateColour(line);
            }
        }

        static void WriteToConsoleInAppropriateColour(string quote)
        {
            bool isLongQuote = quote.Length > 70;

            if (isLongQuote)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine(quote);
        }
    }
}
