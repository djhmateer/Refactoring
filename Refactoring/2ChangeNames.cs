using System.Diagnostics;
using System.IO;

namespace Mateer.ChangeNames
{
    // What does the class do?  (ReadFile)
    public class Program
    {
        // What does the method do?  (ReadFromAFileAndDisplayQuote)
        public static void Main()
        {
            // What does the string array represent?  an array of lines of quotes from a file
            // (lines)
            string[] reading = File.ReadAllLines(@"..\..\quotes.csv");
            for (int i = 0; i < reading.Length; i++)
            {
                var readingaline = reading[i];
                Debug.WriteLine(readingaline);
            }
        }
    }
}
