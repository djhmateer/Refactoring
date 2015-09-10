using System;

namespace QuoteImporterFunctional
{
    // A spike to get decorator working
    public class ImporterFunctionalDecorator
    {
        // Logging using a decorator - passing in 1 parameter
        private static void Main()
        {
            // Function - given a string, return a string.
            // s and t represent the inbound parameters into ParseLine and Log (not needed)
            // line is inbound parameter into ParseLineLogger
            Func<string, string> run = line => ParseLineLogger(s => ParseLine(s), t => Log(t), line);
            string result = run("blah");
        }

        public static string ParseLineLogger(Func<string, string> parseLine, Action<string> log, string line)
        {
            Console.WriteLine($"Start ParseLine with line: {line}");
            string result = parseLine(line);
            Console.WriteLine($"Result: {result}");
            Console.WriteLine("End ParseLine");
            return result;
        }

        public static string ParseLine(string line)
        {
            return line + " ok";
        }

        public static void Log(string message)
        {
            Console.WriteLine($"log: {message}");
        }
    }
}
