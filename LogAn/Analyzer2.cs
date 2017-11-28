using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogAn
{
    public class LogAnalyzer2
    {
        IWebService mWebService; private ILogger _logger;
        private IWebService _webService;
        public LogAnalyzer2(ILogger logger, IWebService webService)
        {
            _logger = logger;
            _webService = webService;
        }
        public int MinNameLength { get; set; }
        public void Analyze(string filename)
        {
            if (filename.Length < MinNameLength)
            {
                try
                {
                    _logger.LogError(
                    string.Format("Filename too short: {0}", filename));
                }
                catch (Exception e)
                {
                    _webService.Write("Error From Logger: " + e);
                }
            }
        }

    }
}
