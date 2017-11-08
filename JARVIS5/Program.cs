using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Reflection;
namespace JARVIS5
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(System.Reflection.Assembly.GetEntryAssembly().Location);
            bool programRunning = true;
            string userInput = "";
            StatusObject SO_PrimaryDatabaseSetup = new StatusObject();
            JARVISDataSource primaryDataSource;
            JARVISDataSource activeDataSource = null;
            Dictionary<string, JARVISDataSource> userDefinedDataSources = new Dictionary<string, JARVISDataSource>();
            /*Execution of all StartUp Files*/
            SO_PrimaryDatabaseSetup = JARVISConfig.ConfigureJARVISDatabase();
            JARVISConfig.CopyExecutable();
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
                    if (!userDefinedDataSources.ContainsKey("work"))
                    {
                        userDefinedDataSources.Add("work", new JARVISDataSource("sql2008kl", "shawn_db", "sa", "password"));
                    }
                    if (!userDefinedDataSources.ContainsKey("home"))
                    {
                        userDefinedDataSources.Add("home", new JARVISDataSource("asus", "JARVIS5", "shawn_tan", "root"));
                    }
                    JARVISLogging.LogCommand(userInput);
                    if (userInput != "exit")
                    {
                        List<string> commandParameters = userInput.Split(' ').ToList();
                        string primaryCommand = commandParameters.ElementAtOrDefault(0);
                        /*Command Sets*/
                        if (primaryCommand == "help")
                        {
                            string commandGroup = commandParameters.ElementAtOrDefault(1);
                            string commandName = commandParameters.ElementAtOrDefault(2);
                            if (commandGroup != null)
                            {
                                JARVISRuntime.Help(commandGroup);

                            }
                            else
                            {
                                JARVISRuntime.Help();
                            }

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
                            else if (secondaryCommand == "setactive")
                            {
                                activeDataSource = new JARVISDataSource(server, database, userID, password);
                            }
                            else if (secondaryCommand == "clearactive")
                            {
                                activeDataSource = null;
                            }
                            else
                            {
                                Console.WriteLine("{0} {1} is not a recognized command", primaryCommand, secondaryCommand);
                            }
                        }
                        else if (primaryCommand == "findtable")
                        {
                            string secondaryCommand = commandParameters.ElementAtOrDefault(1);

                            if (secondaryCommand == "columnname")
                            {
                                string target = userInput.Replace("findtable columnname", "").Trim();
                                Console.WriteLine(target);
                                JARVISDataSource TargetDataSource = new JARVISDataSource("sql2008kl", "claims_dev", "sa", "password");
                                TargetDataSource.SearchTablesByColumnName(target);

                            }
                            else if (secondaryCommand == "tablename")
                            {

                            }
                        }
                        else if (primaryCommand == "read")
                        {
                            string secondaryCommand = commandParameters.ElementAtOrDefault(1);
                            string filePath = userInput.Replace("read", "").Replace(secondaryCommand, "").Trim();
                            if (secondaryCommand == "csvfile")
                            {
                                JARVISFile targetFile = new JARVISFile(filePath);
                                StatusObject SO_ReadFile = targetFile.ReadCSV();
                                if (SO_ReadFile.Status == StatusCode.FAILURE)
                                {
                                    Console.WriteLine(SO_ReadFile.ErrorStackTrace);
                                }
                            }
                            else if (secondaryCommand == "textfile")
                            {
                                JARVISFile targetFile = new JARVISFile(filePath);
                                StatusObject SO_ReadFile = targetFile.AnalyzeRequestAudit(activeDataSource);
                                if (SO_ReadFile.Status == StatusCode.FAILURE)
                                {
                                    Console.WriteLine(SO_ReadFile.ErrorStackTrace);
                                }
                            }
                        }
                        else if (primaryCommand == "web")
                        {
                            string secondaryCommand = commandParameters.ElementAtOrDefault(1);
                            Console.WriteLine(secondaryCommand);
                            StatusObject SO_GetRequest = JARVISWeb.HttpGet(secondaryCommand);
                            if (SO_GetRequest.Status == StatusCode.FAILURE)
                            {
                                Console.WriteLine(SO_GetRequest.ErrorStackTrace);
                            }
                        }
                        else if (primaryCommand == "thread")
                        {

                        }
                        else if (primaryCommand == "wordlist")
                        {
                            string secondaryCommand = commandParameters.ElementAtOrDefault(1);
                            string targetDataSource = commandParameters.ElementAtOrDefault(2);
                            string firstLetter = commandParameters.ElementAtOrDefault(3);
                            string wordLength = commandParameters.ElementAtOrDefault(4);
                            if(secondaryCommand == "buildtables")
                            {
                                StatusObject SO_BuildTable = JARVISCryptography.BuildStringPermutationTable(userDefinedDataSources[targetDataSource]);
                                if (SO_BuildTable.Status == StatusCode.FAILURE)
                                {
                                    Console.WriteLine(SO_BuildTable.ErrorStackTrace);
                                }
                            } 
                            else if (secondaryCommand == "populatetables")
                            {
                                JARVISDataSource Storage = userDefinedDataSources[targetDataSource];
                                StatusObject DictionaryBuilder = JARVISCryptography.PopulateStringPermutationTable(wordLength.ToString(), firstLetter, Storage);
                                if (DictionaryBuilder.Status == StatusCode.FAILURE)
                                {
                                    Console.WriteLine(DictionaryBuilder.ErrorStackTrace);
                                }
                            }
                            else if (secondaryCommand == "buildbatchfiles")
                            {
                                wordLength = commandParameters.ElementAtOrDefault(3);
                                StatusObject SO_BuildBatchFiles = JARVISCryptography.CreateStringPermutationBatchFiles(wordLength, targetDataSource);
                                if (SO_BuildBatchFiles.Status == StatusCode.FAILURE)
                                {
                                    Console.WriteLine(SO_BuildBatchFiles.ErrorStackTrace);
                                }
                            }
                            else if (secondaryCommand == "cleartables")
                            {
                                StatusObject SO_ClearTables = JARVISCryptography.ClearTables(userDefinedDataSources[targetDataSource]);
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
                            Console.ReadKey();
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
                    Console.ReadKey();
                }
            }
        }
    }
}
