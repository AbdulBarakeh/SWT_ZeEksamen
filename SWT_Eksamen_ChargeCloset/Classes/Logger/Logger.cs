using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Classes.Logger
{
    class Logger : ILogger
    {
        private string _logFile = "logfile.txt";
        public void log(string logline)
        {
            using (var writer = File.AppendText(_logFile))
            {
                writer.WriteLine(logline);
            }
        }

    }
}
