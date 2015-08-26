using System;
using System.IO;

namespace Mateer.ChangeNames
{
    class ReadFile
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
