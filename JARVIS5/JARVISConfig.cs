using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace JARVIS5
{
    public static class JARVISConfig
    {
        private static string StartUpFolderPath = @"C:\JARVIS5\Config";
        public static StatusObject StartUpDiagnostics()
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
        public static StatusObject ConfigureJARVISDatabase()
        {
            StatusObject SO = new StatusObject();
            try
            {
                if (!File.Exists(String.Format(@"{0}\Database.txt", StartUpFolderPath)))
                {
                    Directory.CreateDirectory(StartUpFolderPath);
                    string Server;
                    string Database;
                    string UserID;
                    string Password;
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("START: Link JARVIS5 to Database");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.Write("Server: ");
                    Server = Console.ReadLine();
                    Console.Write("Database: ");
                    Database = Console.ReadLine();
                    Console.Write("UserID  (Press enter to skip if using Windows Authentication): ");
                    UserID = Console.ReadLine();
                    Console.Write("Password (Press enter to skip if using Windows Authentication): ");
                    Password = Console.ReadLine();
                    JARVISDataSource PrimaryDataSource = new JARVISDataSource(Server, Database, UserID, Password);
                    PrimaryDataSource.GetSQLConnection();
                    JARVISFile StartupDatabase = new JARVISFile(String.Format(@"{0}\Database.txt", StartUpFolderPath));
                    StartupDatabase.Overwrite(PrimaryDataSource.GetConnectionString());
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("END: Link JARVIS5 to Database");
                    Console.WriteLine("----------------------------------------------------------");
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                SO = new StatusObject(StatusCode.FAILURE, "", e.Message, e.ToString());
            }
            return SO;
        }
        public static StatusObject CreateDesktopShortcut()
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
        public static StatusObject SetActiveDataSource()
        {
            StatusObject SO = new StatusObject();
            try
            {
                string Server;
                string Database;
                string UserID;
                string Password;
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("START: Set Active DataSource");
                Console.WriteLine("----------------------------------------------------------");
                Console.Write("Server: ");
                Server = Console.ReadLine();
                Console.Write("Database: ");
                Database = Console.ReadLine();
                Console.Write("UserID  (Press enter to skip if using Windows Authentication): ");
                UserID = Console.ReadLine();
                Console.Write("Password (Press enter to skip if using Windows Authentication): ");
                Password = Console.ReadLine();
                JARVISDataSource PrimaryDataSource = new JARVISDataSource(Server, Database, UserID, Password);
                PrimaryDataSource.GetSQLConnection();
                Console.WriteLine("----------------------------------------------------------");
                Console.WriteLine("END:  Set Active DataSource");
                Console.WriteLine("----------------------------------------------------------");
                SO.UDDynamic = PrimaryDataSource;
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "", e.Message, e.ToString());
            }
            return SO;
        }
    }
}
