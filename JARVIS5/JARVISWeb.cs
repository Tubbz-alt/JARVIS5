using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace JARVIS5
{
    public static class JARVISWeb
    {
        public static StatusObject HttpGet(string TargetURL)
        {
            StatusObject SO = new StatusObject();
            try
            {
                for (int i = 0; i < 10000000; i++)
                {
                    WebRequest GetRequest = WebRequest.Create(TargetURL);
                    Stream RequestContent = GetRequest.GetResponse().GetResponseStream();
                    StreamReader RequestContentReader = new StreamReader(RequestContent);
                    Console.WriteLine(RequestContentReader.ReadToEnd());
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "JARVISWeb_HttpGet_FAILURE", e.Message, e.ToString());
            }
            return SO;
        }
    }
}
