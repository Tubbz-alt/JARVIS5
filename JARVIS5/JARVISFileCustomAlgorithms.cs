﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace JARVIS5
{
    public partial class JARVISFile
    {
        private string CustomAlgorithmLogPath = @"C:\JARVIS5\FileCustomAlgorithmLogs";
        public StatusObject AnalyzeClaimAudit()
        {
            StatusObject SO = new StatusObject();
            try
            {
                Directory.CreateDirectory(CustomAlgorithmLogPath);
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine("Claim Audit Analysis Start");
                Console.WriteLine("----------------------------------------------------------------------------");
                DateTime StartTime = DateTime.Now;
                FileStream TargetFile = File.Open(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BufferedStream TargetFileBuffered = new BufferedStream(TargetFile);
                StreamReader TargetFileBufferedReader = new StreamReader(TargetFileBuffered);
                
                string Record;
                int Count = 0;
                List<int> ErrorLines = new List<int>();
                List<string> ErrorLineDetails = new List<string>();
                while ((Record = TargetFileBufferedReader.ReadLine()) != null)
                {
                    List<string> Fields = Record.Split('^').ToList();
                    if(Fields.Count != 11)
                    {
                        Console.WriteLine(Record);
                        Thread.Sleep(3000);
                    }
                    else
                    {
                        ErrorLines.Add(Count);
                        StreamWriter ErrorDetails = new StreamWriter(String.Format(@"{0}\AnalyzeClaimAuditErrorDetails.txt", CustomAlgorithmLogPath), append: true);
                        ErrorDetails.WriteLine(Record);
                        ErrorDetails.Close();
                        Console.WriteLine(Fields.Count);
                    }
                    Count++;
                }
                DateTime EndTime = DateTime.Now;
                Console.WriteLine("----------------------------------------------------------------------------");
                Console.WriteLine("Claim Audit Analysis End. Time Elapsed: {0} Records: {1}", (StartTime - EndTime).Milliseconds, Count);
                Console.WriteLine("----------------------------------------------------------------------------");
                StreamWriter ErrorLog = new StreamWriter(String.Format(@"{0}\AnalyzeClaimAuditErrors.txt", CustomAlgorithmLogPath), append: true);
                foreach(int Error in ErrorLines)
                {
                    ErrorLog.WriteLine(Error);
                    ErrorLog.Close();
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "JARVISFILECUSTOMALGO_ANALYZECLAIMAUDIT_FAILURE", e.Message, e.ToString());
            }
            return SO;
        }
        public StatusObject AnalyzeRequestAudit()
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "JARVISFILECUSTOMALGO_ANALYZEREQUESTAUDIT_FAILURE", e.Message, e.ToString());
            }
            return SO;
        }
    }
}
