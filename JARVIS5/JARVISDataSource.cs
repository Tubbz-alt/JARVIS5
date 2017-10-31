using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    public partial class JARVISDataSource
    {
        public string Server { get; set; }
        public string Database { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        public string AuthType { get; }
        private string StoredProcedureSavePath = @"C:\JARVIS5\UserDefinedDataSources\StoredProcedures";
        private string TableSavePath = @"";
        private string QuerySavePath = @"";
        public JARVISDataSource(string Server, string Database)
        {
            this.Server = Server;
            this.Database = Database;
            this.AuthType = "winauth";
        }
        public JARVISDataSource(string Server,string Database, string UserID, string Password)
        {
            this.Server = Server;
            this.Database = Database;
            this.UserID = UserID;
            this.Password = Password;
            this.AuthType = "sqlauth";
        }
        public string GetConnectionString()
        {
            string ConnectionString = "";
            if (this.AuthType == "winauth")
            {
                ConnectionString = String.Format(
                    "server={0};database={1};trusted_connection=true",
                    this.Server,
                    this.Database);
            }
            else if (this.AuthType == "sqlauth")
            {
                ConnectionString = String.Format(
                    "server={0};database={1};user id={2};password={3}",
                    this.Server,
                    this.Database,
                    this.UserID,
                    this.Password);
            }
            else
            {
                ConnectionString = null;
            }
            return ConnectionString;
        }
        public SqlConnection GetSQLConnection()
        {
            SqlConnection NewConnection = new SqlConnection(this.GetConnectionString());
            try
            {
                NewConnection.Open();
                NewConnection.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                NewConnection = null;
            }
            return NewConnection;
        }
        public StatusObject GetStoredProcedures()
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
        public StatusObject GetTables()
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
        public StatusObject GetIndexedSelectQueries()
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
        public StatusObject SearchTables()
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
    }
}
