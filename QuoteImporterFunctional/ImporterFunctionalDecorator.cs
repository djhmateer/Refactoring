using System;

namespace QuoteImporterFunctional
{
    public class ImporterFunctionalDecorator
    {
        private static void Main()
        {
            // **Here - want to wrap QuoteImporter in a decorator so can log

            // Action is like a function which can take parameters but doesn't return anything.
            // Passing in a 2 lambda expression (anonymous function) 
            // 1. which console writes
            // 2. Log
            //Action run = () => QuoteImporter();
            Action run = () => QuoteImporterLogger(() => QuoteImporter(), s => Log(s));
            run();
        }

        public static void QuoteImporterLogger(Action quoteImporter, Action<string> log)
        {
            log("Start QuoteImporter");
            quoteImporter();
            log("End QuoteImporter");
        }

        public static void QuoteImporter()
        {
            Console.WriteLine("QuoteImporter");
        }

        public static void Log(string message)
        {
            Console.WriteLine($"log: {message}");
        }
    }
}
