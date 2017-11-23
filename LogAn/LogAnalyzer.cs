using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {
        public interface ISendEMail
        {
            void SendEMail(string to, string subject, string body);
        }

        private IWebService mWebService;
        private ISendEMail mSendEMail;

        public LogAnalyzer(IWebService webService, ISendEMail email)
        {
            mWebService = webService;
            mSendEMail = email;
        }

        public bool IsValidLogFileName(string fileName)
        {
            if (!fileName.EndsWith(".SLF", StringComparison.CurrentCultureIgnoreCase))
            {
                return false;
            }
            return true;
        }

        public void Analyze(string filename)
        {
            
            if (filename.Length < 8)
            {
                try
                {
                    mWebService.LogError("Filename too short:" + filename);
                }
                catch (Exception ex)
                {
                    mSendEMail.SendEMail("someone@somewhere.com", "Can't log data", ex.Message);
                }
            }
                
        }
    }
}
