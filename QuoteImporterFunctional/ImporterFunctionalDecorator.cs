using System;

namespace QuoteImporterFunctional
{
    public class ImporterFunctionalDecorator
    {
        private static void Main()
        {
            // Action is like a function which can take parameters but doesn't return anything.
            // Passing in a 2 lambda expression (anonymous function) 
            // 1. which console writes
            // 2. Log
            Action run = () => QuoteImporterLogger(QuoteImporter, Log);
            run();
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
