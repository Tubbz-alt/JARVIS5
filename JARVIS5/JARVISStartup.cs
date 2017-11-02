using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace JARVIS5
{
    public static class JARVISStartup
    {
        private static string StartUpFolderPath = @"C:\JARVIS5\StartUp";
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
        public static StatusObject ConfigureJARVISDataSource()
        {
            StatusObject SO = new StatusObject();
            try
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
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                SO = new StatusObject(StatusCode.FAILURE, "", e.Message, e.ToString());
            }
            return SO;
        }
    }
}
