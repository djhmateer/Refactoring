using System;

namespace QuoteImporterFunctional
{
    class DelegateSpike
    {
        // you can define your own delegate for a nice meaningful name, but the
        // generic delegates (Func, Action, Predicate) are all defined already
        public delegate string ParseLine(string value);

        public static void Main()
        {
            ParseLine parseLine = s => s + ", Hello!";
            var result = parseLine("Dave");
            Console.WriteLine(result);

            // both work fine for taking methods, lambdas, etc.
            Func<string, string> parseLine2 = s => s + ", Hello!";
            var result2 = parseLine2("Dave");
            Console.WriteLine(result2);
        }
    }
}
