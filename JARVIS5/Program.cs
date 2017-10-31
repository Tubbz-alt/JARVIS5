using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    class Program
    {
        static void Main(string[] args)
        {
            bool programRunning = true;
            string userInput = "";
            /*Execution of all StartUp Files*/

            /*Execution of batch files first*/
            foreach(string arg in args)
            {
                try
                {
                    Console.WriteLine(arg);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            /*Regular Program Execution*/
            Console.Write("Enter Command: ");
            userInput = Console.ReadLine();
            while(programRunning)
            {
                try
                {
                    if (userInput != "exit")
                    {
                        List<string> commandParameters = userInput.Split(' ').ToList();
                        string primaryCommand = commandParameters.ElementAtOrDefault(0);
                        /*Command Sets*/
                        if (primaryCommand == "help")
                        {

                        }
                        else if (primaryCommand == "datasource")
                        {
                            string secondaryCommand = commandParameters.ElementAtOrDefault(1);
                            JARVISDataSource newDataSource = new JARVISDataSource("sql2008kl", "claims_dev", "sa", "password");
                            newDataSource.GetSQLConnection();
                        }
                        else
                        {
                            Console.WriteLine("{0} is not a recognized command", primaryCommand);
                        }
                        

                        Console.Write("Enter Command: ");
                        userInput = Console.ReadLine();
                    }
                    else
                    {
                        Console.WriteLine("Exiting Program");
                        programRunning = false;
                        userInput = "";
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    programRunning = true;
                    userInput = "";
                }
            }
        }
    }
}
