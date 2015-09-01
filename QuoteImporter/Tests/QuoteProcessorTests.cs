using System;
using Xunit;

namespace QuoteImporter
{
    public class QuoteProcessorTests
    {
        private readonly QuoteProcessor processor;

        public QuoteProcessorTests()
        {
            IEmailler fakeEmailler = new FakeEmailler();
            ILog fakeLogger = new FakeLogger(fakeEmailler);
            processor = new QuoteProcessor(fakeLogger);
        }

        [Fact]
        public void ParseLine_AValidLine_ShouldReturnQuote()
        {
            // Arrange
            string line = "Basic,Programming in Basic causes brain damage - Edsger Wybe Dijkstra";

            // Act
            var result = processor.ParseLine(line);

            // Assert
            Assert.Equal("Basic", result.Title);
            Assert.Equal("Programming in Basic causes brain damage - Edsger Wybe Dijkstra", result.Body);
        }

        [Fact]
        public void ParseLine_AnEmptyLine_ShouldThrow()
        {
            string line = "";

            Assert.Throws<ApplicationException>(() => processor.ParseLine(line));
        }

        [Fact]
        public void ParseLine_TooManyCommas_ShouldThrow()
        {
            string line = "asdf,asdf,asdf";

            Assert.Throws<ApplicationException>(() => processor.ParseLine(line));
        }
    }
}
