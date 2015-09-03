using System;
using System.Collections.Generic;

namespace QuoteImporterFunctional
{
    public static class Importer
    {
        private static void Main()
        {
            Action runProcessing = () => RunProcessing(Log, GetCustomers, CreateReport, SendEmail);
            runProcessing();
        }

        public static void RunProcessing(Action<string> log,
            Func<IEnumerable<string>> getCustomers,
            Func<string, string> createReport,
            Action<string, string> sendEmail)
        {
            log("test");
            var customers = getCustomers();
            foreach (var customer in customers)
            {
                log(customer);
                var report = createReport(customer);
                log(report);
                sendEmail("a@b.com", report);
            }
        }

        public static void SendEmail(string toAddress, string body)
        {
            Console.WriteLine("Sent Email to: {0}, Body: '{1}'", toAddress, body);
        }

        public static void Log(string message)
        {
            Console.WriteLine("log: {0}", message);
        }

        public static string CreateReport(string customerName)
        {
            return customerName + " done!";
        }

        public static IEnumerable<string> GetCustomers()
        {
            yield return "dave";
            yield return "bob";
            yield return "alice";
        }
    }
}