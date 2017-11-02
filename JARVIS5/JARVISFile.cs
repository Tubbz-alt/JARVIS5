﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace JARVIS5
{
    public partial class JARVISFile
    {
        public string FilePath { get; set; }
        public string FileExtension { get; set; }
        public JARVISFile(string FilePath)
        {
            this.FilePath = FilePath;
        }
        public StatusObject Read()
        {
            StatusObject SO = new StatusObject();
            try
            {
                FileStream TargetFile = File.Open(this.FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                BufferedStream TargetFileBuffered = new BufferedStream(TargetFile);
                StreamReader TargetFileBufferedReader = new StreamReader(TargetFileBuffered);
                string line;
                while ((line = TargetFileBufferedReader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "JARVISFILEREAD_FAILURE", e.Message, e.ToString());
            }
            return SO;
        }
        public StatusObject Write()
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch (Exception e)
            {

            }
            return SO;
        }
        public StatusObject Overwrite(string Data)
        {
            StatusObject SO = new StatusObject();
            try
            {
                StreamWriter Target = new StreamWriter(this.FilePath);
                Target.WriteLine(Data);
                Target.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "JARVISFILE_OVERWRITE_FAILURE", e.Message, e.ToString());
            }
            return SO;
        }
    }
}