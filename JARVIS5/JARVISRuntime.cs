using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Threading;
namespace JARVIS5
{
    public static class JARVISRuntime
    {
        public static Dictionary<string, Thread> ThreadDictionary { get; set; }
        public static StatusObject GetTypes()
        {
            StatusObject SO = new StatusObject();
            return SO;
        }
        public static StatusObject ExecuteMethod(string TypeName, string MethodName, string Parameters)
        {
            StatusObject SO = new StatusObject();
            try
            {
                Assembly JARVISAssembly = Assembly.GetExecutingAssembly();

            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject Help()
        {
            StatusObject SO = new StatusObject();
            try
            {
                Assembly JARVISAssembly = Assembly.GetExecutingAssembly();
                Type[] JARVISTypes = JARVISAssembly.GetTypes();
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("Available Commands");
                Console.WriteLine("------------------------------------------------------");
                foreach (Type JARVISType in JARVISTypes)
                {
                    if (JARVISType.Name.Contains("JARVIS"))
                    {
                        Console.WriteLine(JARVISType.Name);
                    }
                }
                Console.WriteLine("------------------------------------------------------");
                Console.WriteLine("Available Commands");
                Console.WriteLine("------------------------------------------------------");
            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject Help (string TypeName)
        {
            StatusObject SO = new StatusObject();
            try
            {
                Assembly JARVISAssembly = Assembly.GetExecutingAssembly();
                Type JARVISType = JARVISAssembly.GetType(String.Format("JARVIS5.JARVIS{0}", TypeName));
                if (JARVISType != null)
                {
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("JARVIS5 Help");
                    Console.WriteLine("------------------------------------------------------");
                    MethodInfo[] JARVISMethods = JARVISType.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).Where(m => !m.IsSpecialName).ToArray();
                    foreach (MethodInfo JARVISMethod in JARVISMethods)
                    {
                        if (!(
                            JARVISMethod.Name == "ToString" ||
                            JARVISMethod.Name == "Equals" ||
                            JARVISMethod.Name == "GetHashCode" ||
                            JARVISMethod.Name == "GetType" ||
                            JARVISMethod.Name == "Finalize" ||
                            JARVISMethod.Name == "MemberwiseClone"
                            ))
                        {
                            Console.WriteLine("{0}", JARVISMethod.Name);
                            ParameterInfo[] JARVISMethodParameters = JARVISMethod.GetParameters();
                            if (JARVISMethodParameters.Length > 0)
                            {
                                Console.WriteLine("Required Parameters");
                                int ParameterCount = 1;
                                foreach (ParameterInfo JARVISMethodParameter in JARVISMethodParameters)
                                {
                                    Console.WriteLine("{0}\t{1}\t{2}", ParameterCount, JARVISMethodParameter.Name, JARVISMethodParameter.ParameterType);
                                    ParameterCount += 1;
                                }
                            }
                        }
                    }
                    Console.WriteLine("------------------------------------------------------");
                    Console.WriteLine("JARVIS5 Help");
                    Console.WriteLine("------------------------------------------------------");
                }
                else
                {
                    Console.WriteLine("Unable to find command");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return SO;
        }
        public static StatusObject StringToObjectArray(string Parameters)
        {
            StatusObject SO = new StatusObject();
            try
            {

            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject AddThread(string MethodName)
        {
            StatusObject SO = new StatusObject();
            try
            {
                
                
            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static void ExecuteThreadMethod(string MethodName)
        {
            try
            {

            }
            catch(Exception e)
            {
                
            }
        }
    }
}
