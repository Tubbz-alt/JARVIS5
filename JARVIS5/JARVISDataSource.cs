using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace JARVIS5
{
    public partial class JARVISDataSource
    {
        [JsonProperty("Server")]
        public string Server { get; set; }
        [JsonProperty("Database")]
        public string Database { get; set; }
        [JsonProperty("UserID")]
        public string UserID { get; set; }
        [JsonProperty("Password")]
        public string Password { get; set; }
        [JsonProperty("AuthType")]
        public string AuthType { private set; get; }
        private string DataSourceSavePath = @"C:\JARVIS5\UserDefinedDataSources";
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
            if (UserID == null || UserID.Length == 0 || Password == null || Password.Length == 0)
            {
                this.AuthType = "winauth";
            }
            else
            {
                this.UserID = UserID;
                this.Password = Password;
                this.AuthType = "sqlauth";
            }
        }
        public JARVISDataSource(string ConnectionString)
        {
            List<string> ConnectionStringParameters = ConnectionString.Split(';').Select(x => x.Trim()).ToList();
            if(ConnectionStringParameters.Count == 2)
            {

            }
            else if (ConnectionStringParameters.Count == 4)
            {

            }
            else
            {
                this.Server = null;
                this.Database = null;
                this.UserID = null;
                this.Password = null;
                this.AuthType = null;
            }
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
        public string GetJSONString()
        {
            string JSONString = "";
            try
            {
                JSONString = JsonConvert.SerializeObject(this);
            }
            catch(Exception e)
            {
                JSONString = null;
            }
            return JSONString;
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
                string SaveDirectory = String.Format(@"{0}\{1}\StoredProcedures", DataSourceSavePath, this.Database);
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
                        StreamWriter StoredProcedureTextFile = new StreamWriter(String.Format(@"{0}\{1}.sql", SaveDirectory, StoredProcedureName));
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
        public StatusObject SearchTablesForValue()
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
        public StatusObject SearchTablesByColumnName(string TableNames)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection NewConnection = GetSQLConnection();
                List<string> TableNameList = TableNames.Split(',').ToList();
                
                foreach (string TableName in TableNameList)
                {
                    SqlCommand TableSearchCommand = new SqlCommand(
                        String.Format("select TABLE_NAME, COLUMN_NAME from information_schema.columns where COLUMN_NAME like '{0}'", TableName),
                        NewConnection);
                    NewConnection.Open();
                    SqlDataReader TableSearchCommandReader = TableSearchCommand.ExecuteReader();
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine("Search results for {0}", TableName);
                    Console.WriteLine("-----------------------------------------------------");
                    while (TableSearchCommandReader.Read())
                    {
                        Console.WriteLine("{0} {1}", TableSearchCommandReader[0], TableSearchCommandReader[1]);
                    }
                    Console.WriteLine("-----------------------------------------------------");
                    Console.WriteLine("Search results for {0}", TableName);
                    Console.WriteLine("-----------------------------------------------------");
                    NewConnection.Close();
                }   
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            return SO = new StatusObject();
        }
        public StatusObject SearchStoredProcedures()
        {
            StatusObject SO = new StatusObject();
            return SO;
        }
        public StatusObject SaveToTextFile()
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
        public StatusObject ExecuteQuery(string Query)
        {
            StatusObject SO = new StatusObject();
            try
            {
                SqlConnection DataSourceConnection = GetSQLConnection();
                SqlCommand CommandToExecute = new SqlCommand(Query, DataSourceConnection);
                DataSourceConnection.Open();
                SqlDataReader CommandToExecuteReader = CommandToExecute.ExecuteReader();
                while (CommandToExecuteReader.Read())
                {
                    // Do something
                }
                DataSourceConnection.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "DataSource_ExecuteQuery_FAILURE", e.Message, e.ToString());
            }
            return SO;
        }
    }
}
