using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

// Ctrl K D (Edit, Advanced)
// Alt E I O A (Edit, Intellisense..)
namespace Mateer.Reformat
{
public class Program
{
    public static void Main(string[] args)
    {
      var reading = File.ReadAllLines(@"..\..\quotes.csv");
        for (int index = 0; index < reading.Length; index++)
        {
           var readingaline = reading[index];
            Console.WriteLine(readingaline);
        }
    }
}
}


