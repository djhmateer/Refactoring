using System;
using System.IO;

namespace ChangeNames
{
    class ReadFileRefactored
    {
        static void Main()
        {
            var fileTextLines = File.ReadAllLines(@"..\..\quotes.csv");
            foreach (var line in fileTextLines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
