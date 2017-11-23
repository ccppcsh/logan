using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit;
using NUnit.Framework;
using static LogAn.LogAnalyzer;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        /*[TestCase("filewithgoodextension.SLF", true)]
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
        */

        [Test]
        public void Analyze_WebServiceThrows_SendsEMail()
        {
            FakeWebService fakeWebService = new FakeWebService();
            fakeWebService.ToThrow = new Exception("fake exception");

            FakeEMailService mockEmail = new FakeEMailService();

            LogAnalyzer logAnalyzer = new LogAnalyzer(fakeWebService, mockEmail);

            logAnalyzer.Analyze("123.ext");

            StringAssert.Contains("someone@somewhere.com", mockEmail.To);
            StringAssert.Contains("fake exception", mockEmail.Body);
            StringAssert.Contains("Can't log data", mockEmail.Subject);
        }

        public class FakeEMailService : ISendEMail
        {
            public String To;
            public String Subject;
            public String Body;
            public void SendEMail(string to, string subject, string body)
            {
                To = to;
                Subject = subject;
                Body = body;
            }
        }
        public class FakeWebService : IWebService
        {
            public Exception ToThrow;
            public void LogError(string message)
            {
                if (ToThrow != null)
                    throw ToThrow;
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
