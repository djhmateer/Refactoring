using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mateer.ReadFile 
{
class Program
{
    static void Main(string[] args)
    {
        var reading = File.ReadAllLines(@"..\..\quotes.csv");
        foreach (var readingaline in reading)
        {
            Console.WriteLine(readingaline);
        }
    }
}
}


