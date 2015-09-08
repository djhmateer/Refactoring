using System;

namespace QuoteImporter.DecoratorSpike
{
    public class DecoratorSpikeCompositionRoot
    {
        static void CompositionRoot()
        {
            // Instead of calling Processor.ReadFile directly we call the wrapper
            var processorLogger = new ProcessorLogger(new Processor(), new Log());
            string result = processorLogger.ReadFile("file.csv");
        }
    }

    public class ProcessorLogger : IProcessor
    {
        private readonly IProcessor processor;
        private readonly ILog log;

        public ProcessorLogger(IProcessor processor, ILog log)
        {
            this.processor = processor;
            this.log = log;
        }

        public string ReadFile(string fileName)
        {
            this.log.Debug($"start ReadFile fileName: {fileName}");
            string result = this.processor.ReadFile(fileName);
            this.log.Debug($"end ReadFile with result: {result}");
            return result;
        }
    }
    public interface IProcessor { string ReadFile(string fileName); }

    public class Log : ILog
    {
        public void Debug(string message)
        {
            Console.WriteLine($"log: {message}");
        }
    }
    public interface ILog { void Debug(string message); }

    public class Processor : IProcessor
    {
        // Benefit of decorator is we don't have logging cluttering up the ReadFile method
        public string ReadFile(string fileName)
        {
            Console.WriteLine("In ReadFile");
            return "stuff in file";
        }
    }
}
