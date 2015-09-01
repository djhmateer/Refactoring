using System;
using Xunit;

namespace Mateer.QuoteProcessor.Refactored
{
    public class QuoteProcessorTests
    {
        [Fact]
        public void AssertTest()
        {
            Assert.Equal(1, 2);
        }

        [Fact]
        public void ParseLine_AValidLine_ShouldReturnQuote()
        {
            // Arrange
            string line = "Basic,Programming in Basic causes brain damage - Edsger Wybe Dijkstra";

            // Act
            // ParseLine is static.. essentially a helper method/function
            var result = QuoteProcessor.ParseLine(line);

            // Assert
            Assert.Equal("Basic", result.Title);
            Assert.Equal("Programming in Basic causes brain damage - Edsger Wybe Dijkstra", result.Body);
        }

        [Fact]
        public void ParseLine_AnEmptyLine_ShouldThrow()
        {
            string line = "";

            Assert.Throws<ApplicationException>(
                () => QuoteProcessor.ParseLine(line)
                );
        }

        [Fact]
        public void ParseLine_TooManyCommas_ShouldThrow()
        {
            string line = "asdf,asdf,asdf";

            Assert.Throws<ApplicationException>(
                () => QuoteProcessor.ParseLine(line)
                );
        }
    }

}