using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [TestCase("filewithgoodextension.SLF", true)]
        [TestCase("filewithgoodextension.slf", true)]
        [TestCase("filewithbadextension.foo", false)]
        public void IsValidFileName_VariousExtensions_ChecksThem(string file, bool expected)
        {
            LogAnalyzer logAnalyzer = new LogAnalyzer(null);

            bool result = logAnalyzer.IsValidLogFileName(file);

            Assert.AreEqual(result, expected);
        }

        [Test]
        public void IsValidFileName_TooShort_CallsWebService()
        {
            FakeWebService fakeWebService = new FakeWebService();
            LogAnalyzer logAnalyzer = new LogAnalyzer(fakeWebService);

            logAnalyzer.Analyze("123.ext");

            StringAssert.Contains("Filename too short:123.ext", fakeWebService.LastError);
        }

        public class FakeWebService : IWebService
        {
            public String LastError;
            public void LogError(string message)
            {
                LastError = message;
            }
        }


        /*[Test]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result = analyzer
            .IsValidLogFileName("filewithgoodextension.slf");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();
            bool result =
            analyzer
            .IsValidLogFileName("filewithgoodextension.SLF");

            Assert.True(result);
        }
        */
    }
}
