using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    public static class JARVISLogging
    {
        private static string ProgramLogPath = @"C:\JARVIS5\ProgramLogs";
        public static bool LogCommand(string userInput)
        {
            try
            {
                Directory.CreateDirectory(ProgramLogPath);
                string CommandLogFilePath = String.Format(@"{0}\Commands.txt", ProgramLogPath);
                StreamWriter CommandLogFile = new StreamWriter(CommandLogFilePath, append: true);
                CommandLogFile.WriteLine("{0}\t{1}", DateTime.Now, userInput);
                CommandLogFile.Close();
                return true;
            }
            catch(Exception e)
            {
                Directory.CreateDirectory(ProgramLogPath);
                string CommandLogFilePath = String.Format(@"{0}\Commands.txt", ProgramLogPath);
                StreamWriter CommandLogFile = new StreamWriter(CommandLogFilePath, append: true);
                CommandLogFile.WriteLine("{0}\t{1}", DateTime.Now, e.ToString().Replace("\r\n", " "));
                CommandLogFile.Close();
                return false;
            }
        }
    }
}
