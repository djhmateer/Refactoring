using System;

namespace QuoteImporterFunctional
{
    // A spike to get decorator working
    public class ImporterFunctionalDecorator
    {
        private static void Main()
        {
            // Action is like a function which can take parameters but doesn't return anything.
            // Passing in 2 method group QuoteImporter, Log
            // Passing in 2 lambda expression (anonymous function)  () => QuoteImporter, s => Log(s)
            // Passing in 2 anonymous methods delegate() { QuoteImporter(); }, delegate(string s) {Log(s);};

            // 1. which console writes
            // 2. Log
            //Action run = () => QuoteImporterLogger(QuoteImporter, Log);
            //run();

            // test out another function with different signature ie returns something
            //Func<string, string> thing = s => ParseLine(s);
            //string result = thing("test");
            //Console.WriteLine(result);

            Func<string, string> run = s => ParseLineLogger(s, ParseLine, Log);
            run("blah");
        }

        public static string ParseLineLogger(string input, Func<string, string> parseLine, Action<string> log)
        {
            Console.WriteLine($"Start ParseLine with input {input}");
            string result = parseLine(input);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("End ParseLine");
            return result;
        }

        public static string ParseLine(string line)
        {
            return line + " ok";
        }

        public static void QuoteImporterLogger(Action quoteImporter, Action<string> log)
        {
            log("Start QuoteImporter");
            quoteImporter();
            log("End QuoteImporter");
        }

        // Don't want logging code cluttering up
        public static void QuoteImporter()
        {
            Console.WriteLine("In QuoteImporter");
        }

        public static void Log(string message)
        {
            Console.WriteLine($"log: {message}");
        }
    }
}
