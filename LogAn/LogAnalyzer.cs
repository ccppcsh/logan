using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer
    {

        private IWebService mWebService;

        public LogAnalyzer(IWebService webService)
        {
            mWebService = webService;
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
                mWebService.LogError("Filename too short:" + filename);
        }
    }
}
