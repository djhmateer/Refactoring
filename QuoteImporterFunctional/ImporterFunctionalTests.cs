using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuoteImporterFunctional
{
    public class ImporterFunctionalTests
    {
        [Fact]
        public void ReadFileList_ShouldReturn3Lines()
        {
            IEnumerable<string> result = ImporterFunctional.ReadFileListOfLines();
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void ParseLine_AValidLine_ShouldReturnAQuote()
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

        // 2. Testing the top level Orchestration function 
        [Fact]
        public void QuoteImporter_GivenMockFile_ShouldInsertIntoMockDatabase()
        {
            // 1. ReadFileListOfLines mock
            string actualLineFromFile = "";
            Func<IEnumerable<string>> readFileListOfLines = () =>
            {
                var listOfLines = new[] {"title, quote here"};
                actualLineFromFile = listOfLines[0];
                return listOfLines;
            };

            // 2. ParseLine mock
            string actualLineSentToParseLine = "";
            Func<string, Quote> parseLine = s =>
            {
                actualLineSentToParseLine = s;
                var quote = new Quote {Title = "title2", Body = "quote here2"};
                return quote;
            };

            // 3. InsertQuoteIntoDatabase mock
            // using a closure
            IList<Quote> actualQuotes = new List<Quote>();
            Action<Quote> insertQuoteIntoDatabase = quote =>
            {
                actualQuotes.Add(quote);
            };

            // compose.  Action is a delegate - doesn't return anything
            Action run = () => ImporterFunctional.QuoteImporter(
                readFileListOfLines, parseLine, insertQuoteIntoDatabase);
            run();

            // 1. Testing the ReadFileListOfLines mock was called
            Assert.Equal("title, quote here", actualLineFromFile);

            // 2. Testing the mock parser received the data from the ReadFileListOfLines mock
            Assert.Equal("title, quote here", actualLineSentToParseLine);

            // 3. Testing the mock database received the data from the ParseLine mock
            Assert.Equal("title2", actualQuotes[0].Title);
            Assert.Equal("quote here2", actualQuotes[0].Body);
        }
    }
}
