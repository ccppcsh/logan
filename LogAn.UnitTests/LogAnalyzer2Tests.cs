using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzer2Tests
    {
        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            //Arrange
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();

            // Simulate exception on any input
            stubLogger.When(
                logger => logger.LogError(Arg.Any<String>()))
                .Do(info => { throw new Exception("fake exception"); });

            var analyzer = new LogAnalyzer2(stubLogger, mockWebService);

            analyzer.MinNameLength = 10;
            //Act
            analyzer.Analyze("123.txt");

            //Assert
            mockWebService.Received().Write(Arg.Is<String>(s => s.Contains("fake exception")));
        }
    }
}
