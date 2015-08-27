using System.Diagnostics;
using System.IO;

// Ctrl K D
// Alt E I O A
namespace Mateer.Reformat 
{
public class Program
{
public static void Main(string[] args)
{
    var reading = File.ReadAllLines(@"..\..\quotes.csv");
 for (int index = 0; index < reading.Length; index++) {
 var readingaline = reading[index];
        Debug.WriteLine(readingaline);
    }
}
}
}


