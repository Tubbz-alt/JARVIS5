using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace JARVIS5
{
    public enum StatusCode
    {
        SUCCESS,
        FAILURE
    }
    public class StatusObject
    {
        public StatusCode Status { get; set; }
        public string ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public string ErrorStackTrace { get; set; }
        private string StatusObjectLogFilePath = @"C:\JARVIS5\Logs\StatusObjectLogs";
        public StatusObject()
        {
            this.Status = StatusCode.SUCCESS;
        }
        public StatusObject(StatusCode Status, string ErrorCode, string ErrorMessage, string ErrorStackTrace)
        {
            this.Status = Status;
            this.ErrorCode = ErrorCode;
            this.ErrorMessage = ErrorMessage;
            this.ErrorStackTrace = ErrorStackTrace;
        }
        public bool LogError()
        {
            try
            {
                Directory.CreateDirectory(StatusObjectLogFilePath);
                StreamWriter StatusObjectLogFile = new StreamWriter(String.Format(@"{0}\Log.txt", this.StatusObjectLogFilePath), append: true);
                StatusObjectLogFile.WriteLine(String.Format("{0}\t{1}\t{2}", DateTime.Now, this.ErrorCode, this.ErrorMessage));
                StatusObjectLogFile.Close();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }
    }
}
