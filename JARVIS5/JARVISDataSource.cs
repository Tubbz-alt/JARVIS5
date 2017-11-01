using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
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
        public string AuthType { private set; get; }
        private string StoredProcedureSavePath = @"C:\JARVIS5\UserDefinedDataSources\StoredProcedures";
        private string TableSavePath = @"C:\JARVIS5\UserDefinedDataSources\Tables";
        private string QuerySavePath = @"C:\JARVIS5\UserDefinedDataSources\Queries";
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
        public StatusObject ExportStoredProcedures()
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection NewConnection = GetSQLConnection();
                SqlCommand StoredProcedureNamesQuery = new SqlCommand(
                    @"select STOREDPROCNAME=SPECIFIC_NAME from information_schema.routines" +
                    " where routine_type = 'PROCEDURE'" +
                    " and Left(Routine_Name, 3) NOT IN('sp_', 'xp_', 'ms_', 'dt_')" +
                    " order by SPECIFIC_NAME asc", 
                    NewConnection);
                // Get Stored Procedure Names
                List<string> StoredProcedureNamesList = new List<string>();
                NewConnection.Open();
                SqlDataReader StoredProcedureNamesQueryReader = StoredProcedureNamesQuery.ExecuteReader();
                while (StoredProcedureNamesQueryReader.Read())
                {
                    Console.WriteLine(StoredProcedureNamesQueryReader[0].ToString());
                    StoredProcedureNamesList.Add(StoredProcedureNamesQueryReader[0].ToString());
                }
                NewConnection.Close();

                // Get Stored Procedure Text and Copy it to file
                string SaveDirectory = String.Format(@"{0}\{1}", StoredProcedureSavePath, this.Database);
                Directory.CreateDirectory(SaveDirectory);
                foreach (string StoredProcedureName in StoredProcedureNamesList)
                {
                    SqlCommand StoredProcedureTextQuery = new SqlCommand(
                        String.Format(
                            "select OBJECT_DEFINITION(OBJECT_ID('{0}'))", 
                            StoredProcedureName),
                        NewConnection);
                    NewConnection.Open();
                    SqlDataReader StoredProcedureTextQueryReader = StoredProcedureTextQuery.ExecuteReader();
                    while (StoredProcedureTextQueryReader.Read())
                    {
                        StreamWriter StoredProcedureTextFile = new StreamWriter(String.Format(@"{0}\{1}.txt", SaveDirectory, StoredProcedureName));
                        StoredProcedureTextFile.Write(StoredProcedureTextQueryReader[0].ToString());
                        StoredProcedureTextFile.Close();
                    }
                    NewConnection.Close();
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "GetStoredProceduresFailure", e.Message, e.ToString());
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
