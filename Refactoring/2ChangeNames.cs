using System;
using System.IO;

namespace Mateer.ChangeNames
{
    // What does the class do?  (ReadFile)
    public class ReadFile
    {
        // What does the method do?  (ReadFromAFileAndDisplayQuote)
        public static void ReadFromAFileAndDisplayAQuote()
        {
            // What does the string array represent?  an array of lines of quotes from a file
            // (lines)
            //var????
            string[] lines = File.ReadAllLines(@"..\..\quotes.csv");
            foreach (var line in lines)
            {
                Console.WriteLine(line);
            }
        }
    }
}
