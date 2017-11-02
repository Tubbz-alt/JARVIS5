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
            
            JARVISStartup.ConfigureJARVISDataSource();

            /*Execution of batch files first*/
            if(args.Length > 0)
            {
                /*If batch execution, do not require user input*/
                Console.WriteLine("--------------------------------------------------------------------");
                Console.WriteLine("Start Batch Execution");
                Console.WriteLine("--------------------------------------------------------------------");
                userInput = string.Join(" ", args);
                Console.WriteLine(userInput);
            }
            else
            {
                /*If non-batch, require user input*/
                Console.Write("Enter Command: ");
                userInput = Console.ReadLine();
            }
            
            while(programRunning)
            {
                try
                {
                    JARVISLogging.LogCommand(userInput);
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
                            string server = commandParameters.ElementAtOrDefault(2);
                            string database = commandParameters.ElementAtOrDefault(3);
                            string userID = commandParameters.ElementAtOrDefault(4);
                            string password = commandParameters.ElementAtOrDefault(5);
                            JARVISDataSource newDataSource = new JARVISDataSource(server, database, userID, password);
                            if (secondaryCommand == "exportssp")
                            {
                                newDataSource.GetSQLConnection();
                                newDataSource.ExportStoredProcedures();
                            }
                            else if (secondaryCommand == "add")
                            {
                                newDataSource.GetSQLConnection();
                                newDataSource.SaveToTextFile();
                            }
                            else
                            {
                                Console.WriteLine("{0} {1} is not a valid command", primaryCommand, secondaryCommand);
                            }
                        }
                        else if (primaryCommand == "findtable")
                        {
                            string secondaryCommand = commandParameters.ElementAtOrDefault(1);
                            string target = commandParameters.ElementAtOrDefault(2);
                            if(secondaryCommand == "columnname")
                            {
                                JARVISDataSource TargetDataSource = new JARVISDataSource("sql2008kl", "claims_dev", "sa", "password");
                                TargetDataSource.SearchTablesByColumnName(target);

                            }
                            else if (secondaryCommand == "tablename")
                            {

                            }
                        }
                        else if (primaryCommand == "read")
                        {
                            string filePath = userInput.Replace("read", "").Trim();
                            JARVISFile targetFile = new JARVISFile(filePath);
                            StatusObject SO_ReadFile = targetFile.AnalyzeClaimAudit();
                            if(SO_ReadFile.Status == StatusCode.FAILURE)
                            {
                                Console.WriteLine(SO_ReadFile.ErrorStackTrace);
                            }
                        }
                        else
                        {
                            Console.WriteLine("{0} is not a recognized command", primaryCommand);
                        }

                        if (args.Length > 0)
                        {
                            Console.WriteLine("--------------------------------------------------------------------");
                            Console.WriteLine("End Batch Execution");
                            Console.WriteLine("--------------------------------------------------------------------");
                            userInput = "exit";
                        }
                        else
                        {
                            Console.Write("Enter Command: ");
                            userInput = Console.ReadLine();
                        }
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
