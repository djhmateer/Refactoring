using System;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Console.WriteLine(readingaline);
    }
}
}
}


