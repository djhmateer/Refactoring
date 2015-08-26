using System;
using Xunit;

namespace Mateer.QuoteProcessorCompose
{
    public class FakeLogger : ILog
    {
        private readonly IEmailler _emailler;

        public FakeLogger(IEmailler emailler)
        {
            _emailler = emailler;
        }

        public void Debug(string message)
        {
            Console.WriteLine("FakeLogger Debug: " + message);
        }

        public void Exception(string message)
        {
            Console.WriteLine("FakeLogger Exception: " + message);
        }
    }

    public class FakeEmailler : IEmailler
    {
        public void SendEmail(string body)
        {
            Console.WriteLine("FakeEmailler: " + body);
        }
    }

    public class QuoteProcessorServiceTests
    {
        private readonly QuoteProcessorService _service;

        public QuoteProcessorServiceTests()
        {
            IEmailler fakeEmailler = new FakeEmailler();
            ILog fakeLogger = new FakeLogger(fakeEmailler);
            _service = new QuoteProcessorService(fakeLogger);
        }

        [Fact]
        public void ParseLine_AValidLine_ShouldReturnQuote()
        {
            // Arrange
            string line = "Basic,Programming in Basic causes brain damage - Edsger Wybe Dijkstra";

            // Act
            var result = _service.ParseLine(line);

            // Assert
            Assert.Equal("Basic", result.Title);
            Assert.Equal("Programming in Basic causes brain damage - Edsger Wybe Dijkstra", result.Body);
        }

        [Fact]
        public void ParseLine_AnEmptyLine_ShouldThrow()
        {
            string line = "";

            Assert.Throws<ApplicationException>(() => _service.ParseLine(line));
        }

        [Fact]
        public void ParseLine_TooManyCommas_ShouldThrow()
        {
            string line = "asdf,asdf,asdf";

            Assert.Throws<ApplicationException>(() => _service.ParseLine(line));
        }
    }
}

