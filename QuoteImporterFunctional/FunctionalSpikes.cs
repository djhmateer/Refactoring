using System;

namespace QuoteImporterFunctional
{
    class FunctionalSpikes
    {
        private static void Main()
        {
            // Action is like a function which can take parameters but doesn't return anything.
            // Passing in a lambda expression (anonymous function) 
            // which console writes
            Action note1 = () => Console.WriteLine("hello1");
            note1();

            // Passing a parameter to the lambda (anonymous function)
            Action<string> note2 = x => Console.WriteLine($"message is {x}");
            note2("hello2");

            // Method group with the string parameter
            Action<string> log = s => Log(s);
            log("hello from log");

            // Method group - don't need to specify the string parameter!
            Action<string> log2 = Log;
            log2("hello from log2");

            // Anonymous method
            Action<string> log3 = delegate(string s) { Log(s); };
            log("hello from log3");

            // Function with input string, and output string
            // lambda expression taking input and returning an ouput
            Func<string, string> note3 = x =>
            {
                Console.WriteLine($"message is {x}");
                return "success3";
            };
            string result = note3("hello3");
            Console.WriteLine(result);

            // Anonymous method
            Func<string, string> note4 = delegate(string x)
            {
                Console.WriteLine($"message is {x}");
                return "success3";
            };
            note4("hello4");

            // Func is a generic type which encapsulates delegates (callable code)
            Func<float, float> square;
            square = delegate (float x) { return x * x; };
            square = (float x) => x * x;
            square = x => x * x;

            var resultA = square(3);
            Console.WriteLine(resultA);

            // Action is a delegate.  Doesn't return a value
            // Passing the named method into QuoteImporter
            Action runProcessing = () => RunProcessing(Log);
            runProcessing();

            // passing QuoteImporter method into r
            // Passing lambda expression (anonymous method) into QuoteImporter
            Action runProcessing2 = () => RunProcessing(x => Console.WriteLine($"log2: {x}"));
            runProcessing2();

            // Logging using a decorator
            // Action is like a function which can take parameters but doesn't return anything.
            // Passing in 2 method group QuoteImporter, Log
            // Passing in 2 lambda expression (anonymous function)  () => QuoteImporter, s => Log(s)
            // Passing in 2 anonymous methods delegate() { QuoteImporter(); }, delegate(string s) {Log(s);};

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



        public static void RunProcessing(Action<string> log)
        {
            log("test");
        }
    }
}
