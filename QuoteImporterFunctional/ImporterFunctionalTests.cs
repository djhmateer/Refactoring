using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuoteImporterFunctional
{
    public class ImporterFunctionalTests
    {
        // 1. Static functions
        [Fact]
        public void ReadFileList_x_ShouldReturnListOfLines()
        {
            // mocking out log
            // Action is a delegate which doesn't return a value
            // Passing in an lambda (anonymous function) which does nothing
            Action<string> log = s => { };

            IEnumerable<string> result = ImporterFunctional.ReadFileListOfLines(log);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ParseLine_AValidLine_ShouldReturnQuote()
        {
            string line = "Basic,Programming in Basic causes brain damage - Edsger Wybe Dijkstra";

            Quote result = ImporterFunctional.ParseLine(line);

            Assert.Equal("Basic", result.Title);
            Assert.Equal("Programming in Basic causes brain damage - Edsger Wybe Dijkstra", result.Body);
        }

        [Fact]
        public void ParseLine_AnEmptyLine_ShouldThrow()
        {
            string line = "";

            Assert.Throws<ApplicationException>(() => ImporterFunctional.ParseLine(line));
        }

        [Fact]
        public void ParseLine_TooManyCommas_ShouldThrow()
        {
            string line = "asdf,asdf,asdf";

            Assert.Throws<ApplicationException>(() => ImporterFunctional.ParseLine(line));
        }

        // 2. Testing the main function - GetProcessing which has 4 dependencies 
        // mocking out the dependencies, and testing what this main function
        // does to it's dependencies
        [Fact]
        public void RunProcessing_GivenQuoteMock_ShouldInsertIntoMockDatabase()
        {
            // mocking out QuoteImporter's 4 dependencies
            // Action is a delegate which doesn't return a value
            // Passing in an anonymous function which does nothing
            Action<string> log = x => { };

            // mock file reader
            string actualLineFromFile = "";
            Func<IEnumerable<string>> readFileListOfLines = delegate
            {
                var listOfLines = new[] { "title, quote here" };
                actualLineFromFile = listOfLines[0];
                return listOfLines;
            };

            // mock the parser
            string actualLineSentToParser = "";
            Func<string, Quote> parseLine = delegate (string s)
            {
                actualLineSentToParser = s;
                var quote = new Quote { Title = "title2", Body = "quote here2" };
                return quote;
            };

            // Mock the database insert
            // using a closure
            IList<Quote> actualQuotes = new List<Quote>();
            Action<Quote> insertQuoteIntoDatabase = quote =>
            {
                actualQuotes.Add(quote);
            };

            // compose.  Action is a delegate - doesn't return anything
            Action run = () => ImporterFunctional.QuoteImporter(log,
                readFileListOfLines, parseLine, insertQuoteIntoDatabase);
            run();

            // Testing the mock reader is in the state we expect
            Assert.Equal("title, quote here", actualLineFromFile);

            // Testing the mock parser is in the state we expect
            Assert.Equal("title, quote here", actualLineSentToParser);

            // Testing the mock database is in the state we expect
            Assert.Equal("title2", actualQuotes[0].Title);
            Assert.Equal("quote here2", actualQuotes[0].Body);
        }

        //[Fact]
        //public void RunProcessingShouldLog()
        //{
        //    var listOfLogEntries = new List<string>();
        //    // Action is a delegate - doesn't return.  
        //    Action<string> log = m => listOfLogEntries.Add(m);

        //    // Func input of a string, returns string.. 
        //    Func<string, string> createReport = x => x + " report test";

        //    // Func with no input, delegating to an anonymous function, 
        //    // returning an array of 1 string
        //    Func<IEnumerable<string>> getCustomers = () => new[] { "ellie" };

        //    Action<string, string> sendEmail = (toAddress, body) => { };

        //    ImporterFunctional3Spike.QuoteImporter(log, getCustomers, createReport, sendEmail);

        //    // assert logging in working
        //    Assert.Equal(3, listOfLogEntries.Count);
        //    Assert.Equal("test", listOfLogEntries[0]);
        //    Assert.Equal("ellie", listOfLogEntries[1]);
        //    Assert.Equal("ellie report test", listOfLogEntries[2]);
        //}

        //[Fact]
        //public void RunProcessingShouldSendMultipleCorrectEmailBodies()
        //{
        //    Action<string> log = m => { };
        //    Func<string, string> createReport = customer => customer + " done";
        //    Func<IEnumerable<string>> getCustomers = () => new[] { "ellie", "bob" };

        //    // closure
        //    var actualBodyList = new List<string>();
        //    Action<string, string> sendEmail = (toAddress, body) => actualBodyList.Add(body);

        //    ImporterFunctional3Spike.QuoteImporter(log, getCustomers, createReport, sendEmail);

        //    Assert.Equal("ellie done", actualBodyList[0]);
        //    Assert.Equal("bob done", actualBodyList[1]);
        //}
    }
}
