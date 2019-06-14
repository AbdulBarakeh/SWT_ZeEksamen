using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace Classes.Logger
{
    public class Logger : ILogger
    {

        private string _logFile =  Path.GetTempPath()+"logfile.txt";

        public void log(string logline)
        {
            using (StreamWriter writer = File.AppendText(_logFile))
            {
                loggin(logline,writer);
            }

        }


        public void loggin(string logginline, TextWriter writer)
        {
            writer.WriteLine($"{logginline}");
        }

        //Inspiration inddraget fra: https://docs.microsoft.com/en-us/dotnet/standard/io/how-to-open-and-append-to-a-log-file

    }


}