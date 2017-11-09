using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Data.SqlClient;

namespace JARVIS5
{
    public static class JARVISCryptography
    {
        public static string RandomAlgorithmOutputPath = @"C:\JARVIS5\RandomAlgorithmOutput";
        private static string RainbowTableBatchFilePath = @"C:\JARVIS5\Cryptography\RainbowTables\BatchFiles";
        
        public static StatusObject BuildUUIDTable()
        {
            StatusObject SO = new StatusObject();
            try
            {
                JARVISDataSource newDataSource = new JARVISDataSource("sql2008kl", "shawn_db", "sa", "password");
                Guid.NewGuid();
            }
            catch(Exception e)
            {
                
            }
            return SO;
        }
        public static StatusObject BuildHashTable()
        {
            StatusObject SO = new StatusObject();
            try
            {
                // Drop table RainbowTable
                // Create table RainbowTable
                // Dictionary words
                // Take l33t5p34k into consideration
            }
            catch(Exception e)
            {

            }
            return SO;
        }
        public static StatusObject BuildStringPermutationTable(string WordLength, JARVISDataSource DictionaryStorage)
        {
            StatusObject SO = new StatusObject();
            try
            {
                Dictionary<string, string> TableCreationQueries = new Dictionary<string, string>();
                Dictionary<string, string> IndexCreationQueries = new Dictionary<string, string>();
                Dictionary<string, string> HashTypes = new Dictionary<string, string>()
                {
                    { "MD5","32" },
                    { "SHA","40" },
                    { "SHA1","40" },
                    { "SHA2_256","64" },
                    { "SHA2_512","128" }
                };
                int wordLength = Convert.ToInt32(WordLength);
                TableCreationQueries.Add(
                    "TableCreateQuery",
                    @"CREATE TABLE [dbo].[RAINBOW_{0}_{3}_{1}](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [Word] [varchar](20) NOT NULL,
	                    [FirstLetter] [varchar](1) NOT NULL,
	                    [LetterCount] [int] NOT NULL,
	                    [{1}] [varchar]({2}) NULL,
                     CONSTRAINT [PK_RAINBOW_{0}_{3}_{1}] PRIMARY KEY CLUSTERED 
                    (
	                    [ID] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY]");

                IndexCreationQueries.Add(
                    "FirstLetter",
                    @"CREATE NONCLUSTERED INDEX [IX_FIRSTLETTER_{0}_{1}_{2}] ON [dbo].[RAINBOW_{0}_{1}_{2}]
                    (
	                    [FirstLetter] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                IndexCreationQueries.Add(
                    "LetterCount",
                    @"CREATE NONCLUSTERED INDEX [IX_LETTERCOUNT_{0}_{1}_{2}] ON [dbo].[RAINBOW_{0}_{1}_{2}]
                    (
	                    [LetterCount] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                IndexCreationQueries.Add(
                    "HashQuery",
                    @"CREATE NONCLUSTERED INDEX [IX_{2}_{0}_{1}] ON [dbo].[RAINBOW_{0}_{1}_{2}]
                    (
	                    [{2}] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                IndexCreationQueries.Add("IndexWordQuery",
                    @"CREATE UNIQUE INDEX [IX_WORD_{0}_{1}_{2}] ON [dbo].[RAINBOW_{0}_{1}_{2}]
                    (
                        [Word] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");

                string TargetString = "";
                for (int LargeAlphabet = 65; LargeAlphabet <= 90; LargeAlphabet++)
                {
                    TargetString += (char)LargeAlphabet;
                }
                for (int smallAlphabet = 97; smallAlphabet <= 122; smallAlphabet++)
                {
                    TargetString += (char)smallAlphabet;
                }
                for (int Numeric = 48; Numeric <= 57; Numeric++)
                {
                    TargetString += (char)Numeric;
                }
                for (int Symbol = 32; Symbol <= 47; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 58; Symbol <= 64; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 91; Symbol <= 96; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 123; Symbol <= 126; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                //Create Tables
                foreach (char Character in TargetString)
                {
                    foreach(KeyValuePair<string,string> Query in TableCreationQueries)
                    {

                        foreach (KeyValuePair<string, string> HashType in HashTypes)
                        {
                            for (int letterCount = 1; letterCount < wordLength; letterCount++)
                            {
                                Console.WriteLine("Creating Table RAINBOW_{1}_{3}_{2}", Query.Key.Substring(0, 5), (int)Character, HashType.Key, letterCount);
                                Console.WriteLine(String.Format(Query.Value, (int)Character, HashType.Key, HashType.Value, letterCount));
                                StatusObject SO_CreateTable = DictionaryStorage.ExecuteNonReaderQuery(String.Format(Query.Value, (int)Character, HashType.Key, HashType.Value, letterCount));

                                if (SO_CreateTable.Status == StatusCode.FAILURE)
                                {
                                    Console.WriteLine(SO_CreateTable.ErrorMessage);
                                    break;
                                }
                                foreach(KeyValuePair<string,string> IndexQuery in IndexCreationQueries)
                                {
                                    StatusObject SO_CreateIndex = DictionaryStorage.ExecuteNonReaderQuery(
                                    String.Format(
                                        IndexQuery.Value,
                                        (int)Character,
                                        letterCount,
                                        HashType.Key));
                                    if (SO_CreateIndex.Status == StatusCode.FAILURE)
                                    {
                                        Console.WriteLine(SO_CreateIndex.ErrorMessage);
                                        break;
                                    }
                                }
                                
                            }
                        }
                    }
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
            }
            return SO;
        }
        public static StatusObject PopulateStringPermutationTable(string WordLength, string FirstLetter, JARVISDataSource DictionaryStorage)
        {
            StatusObject SO = new StatusObject();
            try
            {
                Dictionary<string, string> HashTypes = new Dictionary<string, string>()
                {
                    { "MD5","32" },
                    { "SHA","40" },
                    { "SHA1","40" },
                    { "SHA2_256","64" },
                    { "SHA2_512","128" }
                };
                string TargetString = "";
                for (int LargeAlphabet = 65; LargeAlphabet <= 90; LargeAlphabet++)
                {
                    TargetString += (char)LargeAlphabet;
                }
                for (int smallAlphabet = 97; smallAlphabet <= 122; smallAlphabet++)
                {
                    TargetString += (char)smallAlphabet;
                }
                for (int Numeric = 48; Numeric <= 57; Numeric++)
                {
                    TargetString += (char)Numeric;
                }
                for(int Symbol = 32; Symbol <=47; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 58; Symbol <= 64; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 91; Symbol <= 96; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 123; Symbol <= 126; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                string ReorderedString = "";
                char FirstCharacter = (FirstLetter == "space") ? ' ' : FirstLetter.ToCharArray()[0];
                ReorderedString += FirstCharacter;
                foreach (char TargetCharacter in TargetString)
                {
                    if(TargetCharacter != FirstCharacter)
                    {
                        ReorderedString += TargetCharacter;
                    }
                }
                var q = ReorderedString.Select(x => x.ToString());
                int size = Convert.ToInt32(WordLength);
                for (int i = 0; i < size - 1; i++)
                    q = q.SelectMany(x => ReorderedString, (x, y) => x + y);

                foreach (var item in q)
                {
                    if(item[0] == FirstCharacter)
                    {
                        foreach(KeyValuePair<string,string> HashType in HashTypes)
                        {
                            string insertQuery = String.Format(
                            @"insert into RAINBOW_{3}_{2}_{4} 
                            (Word,FirstLetter,LetterCount,{4}) 
                            values (
                                '{0}',
                                '{1}',
                                {2},
                                convert(varchar({5}),hashbytes('{4}','{0}'),2))",
                            item.Replace("'", "''"),
                            item[0] == '\'' ? "''" : item[0].ToString(),
                            item.Length,
                            (int)item[0],
                            HashType.Key,
                            HashType.Value);
                            Console.WriteLine(insertQuery);
                            StatusObject SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                            while (SO_AddRecord.Status == StatusCode.FAILURE)
                            {
                                Console.WriteLine(SO_AddRecord.ErrorMessage);
                                SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
            }
            return SO;
        }
        public static StatusObject CreateStringPermutationBatchFiles(string MaxWordLength, string DataSourceName)
        {
            StatusObject SO = new StatusObject();
            try
            {
                string TargetString = "";
                for (int LargeAlphabet = 65; LargeAlphabet <= 90; LargeAlphabet++)
                {
                    TargetString += (char)LargeAlphabet;
                }
                for (int smallAlphabet = 97; smallAlphabet <= 122; smallAlphabet++)
                {
                    TargetString += (char)smallAlphabet;
                }
                for (int Numeric = 48; Numeric <= 57; Numeric++)
                {
                    TargetString += (char)Numeric;
                }
                for (int Symbol = 32; Symbol <= 47; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 58; Symbol <= 64; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 91; Symbol <= 96; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                for (int Symbol = 123; Symbol <= 126; Symbol++)
                {
                    TargetString += (char)Symbol;
                }
                Directory.CreateDirectory(RainbowTableBatchFilePath);
                foreach(char TargetCharacter in TargetString)
                {
                    for(int i = 1; i <= Convert.ToInt32(MaxWordLength); i++)
                    {
                        //RAINBOW_97_10
                        Console.WriteLine("RAINBOW_{0}_{1}.bat", (int)TargetCharacter, i);
                        string ReplacementCharacter = null;
                        bool requiresEscape = false;
                        if(TargetCharacter == ' ')
                        {
                            ReplacementCharacter = "space";
                            requiresEscape = true;
                        }
                        else if (JARVISUniversalDefinitions.BatchFileEscapeCharacters.ContainsKey(TargetCharacter))
                        {
                            ReplacementCharacter = JARVISUniversalDefinitions.BatchFileEscapeCharacters[TargetCharacter];
                            requiresEscape = true;
                        }

                        StreamWriter BatchFile = new StreamWriter(String.Format(@"{0}\RAINBOW_{1}_{2}.bat", RainbowTableBatchFilePath, (int)TargetCharacter, i));
                        string batchInstruction =
                            String.Format(
                                    "{0} wordlist populatetables {1} {2} {3}",
                                    System.Reflection.Assembly.GetEntryAssembly().Location,
                                    DataSourceName,
                                    (requiresEscape) ? ReplacementCharacter : TargetCharacter.ToString(),
                                    i);
                        BatchFile.WriteLine(batchInstruction);
                        BatchFile.Close();
                    }
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "CreateStringPermutationBatchFiles_ERROR", e.Message, e.ToString());
            }
            return SO;
        }
        public static StatusObject ClearTables(JARVISDataSource DictionaryStorage)
        {
            StatusObject SO = new StatusObject();
            try
            {
                string TruncateTableQuery = "select 'drop table '+TABLE_NAME from INFORMATION_SCHEMA.TABLES where TABLE_NAME like 'RAINBOW%'";
                SqlConnection tempConnection = DictionaryStorage.GetSQLConnection();
                SqlCommand TruncateTableCommand = new SqlCommand(TruncateTableQuery, tempConnection);
                tempConnection.Open();
                SqlDataReader Reader = TruncateTableCommand.ExecuteReader();
                while (Reader.Read())
                {
                    Console.WriteLine(Reader[0]);
                    StatusObject SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(Reader[0].ToString());
                    if (SO_AddRecord.Status == StatusCode.FAILURE)
                    {
                        Console.WriteLine(SO_AddRecord.ErrorMessage);
                    }
                }
                tempConnection.Close();
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "ClearTable_ERROR", e.Message, e.ToString());
            }
            return SO;
        }
    }
}
