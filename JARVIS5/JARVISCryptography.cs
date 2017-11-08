using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                var alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
                var q = alphabet.Select(x => x.ToString());
                int size = Convert.ToInt32(WordLength);
                for (int i = 0; i < size - 1; i++)
                    q = q.SelectMany(x => alphabet, (x, y) => x + y);
                
                foreach (var item in q)
                {
                    string insertQuery = String.Format("insert into Dictonary values ('{0}','{1}',{2})", item, item[0], item.Length);
                    DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                    Console.WriteLine(item);
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
            }
            return SO;
        }
        public static StatusObject BuildStringPermutationTable(JARVISDataSource DictionaryStorage)
        {
            StatusObject SO = new StatusObject();
            try
            {
                Dictionary<string, string> TableCreationQueries = new Dictionary<string, string>();
                TableCreationQueries.Add(
                    "TableCreateQuery",
                    @"CREATE TABLE [dbo].[RAINBOW_{0}](
	                    [ID] [int] IDENTITY(1,1) NOT NULL,
	                    [Word] [varchar](255) NOT NULL,
	                    [FirstLetter] [varchar](1) NOT NULL,
	                    [LetterCount] [int] NOT NULL,
	                    [MD5] [varchar](255) NULL,
	                    [SHA] [varchar](255) NULL,
	                    [SHA1] [varchar](255) NULL,
	                    [SHA2_256] [varchar](255) NULL,
	                    [SHA2_512] [varchar](255) NULL,
                     CONSTRAINT [PK_RAINBOW_{0}] PRIMARY KEY CLUSTERED 
                    (
	                    [ID] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
                    ) ON [PRIMARY]");
                TableCreationQueries.Add(
                    "IndexFirstLetterQuery",
                    @"CREATE NONCLUSTERED INDEX [IX_FIRSTLETTER_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [FirstLetter] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add(
                    "IndexLetterCountQuery",
                    @"CREATE NONCLUSTERED INDEX [IX_LETTERCOUNT_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [LetterCount] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add(
                    "IndexMD5Query",
                    @"CREATE NONCLUSTERED INDEX [IX_MD5_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [MD5] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add("IndexSHAQuery",
                    @"CREATE NONCLUSTERED INDEX [IX_SHA_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [SHA] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add("IndexSHA1Query",
                    @"CREATE NONCLUSTERED INDEX [IX_SHA1_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [SHA1] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add("IndexSHA2_256Query",
                    @"CREATE NONCLUSTERED INDEX [IX_SHA2_256_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [SHA2_256] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add("IndexSHA2_512Query",
                    @"CREATE NONCLUSTERED INDEX [IX_SHA2_512_{0}] ON [dbo].[RAINBOW_{0}]
                    (
	                    [SHA2_512] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");
                TableCreationQueries.Add("IndexWordQuery",
                    @"CREATE NONCLUSTERED INDEX [IX_WORD_{0}] ON [dbo].[RAINBOW_{0}]
                    (
                        [Word] ASC
                    )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]");

                string TargetString = "";
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
                        Console.WriteLine("Creating {0} RAINBOW_{1}", Query.Key.Substring(0, 5), (int)Character);
                        StatusObject SO_CreateTable = DictionaryStorage.ExecuteNonReaderQuery(String.Format(Query.Value, (int)Character));
                        if (SO_CreateTable.Status == StatusCode.FAILURE)
                        {
                            Console.WriteLine(SO_CreateTable.ErrorMessage);
                            break;
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
                string TargetString = "";
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
                        string insertQuery = String.Format(
                            @"insert into RAINBOW_{3} (Word,FirstLetter,LetterCount,MD5) values ('{0}','{1}',{2},convert(varchar(255),hashbytes('MD5','{0}'),2))",
                            item.Replace("'", "''"),
                            item[0] == '\'' ? "''" : item[0].ToString(),
                            item.Length,
                            (int)item[0]);
                        Console.WriteLine(insertQuery);
                        StatusObject SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                        while (SO_AddRecord.Status == StatusCode.FAILURE)
                        {
                            Console.WriteLine(SO_AddRecord.ErrorMessage);
                            SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
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
                string TargetString = "";
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
                foreach(char TargetCharacter in TargetString)
                {
                    string truncateQuery = String.Format("truncate table RAINBOW_{0}", (int)TargetCharacter);
                    Console.WriteLine(truncateQuery);
                    StatusObject SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(truncateQuery);
                    if (SO_AddRecord.Status == StatusCode.FAILURE)
                    {
                        Console.WriteLine(SO_AddRecord.ErrorMessage);
                    }
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "ClearTable_ERROR", e.Message, e.ToString());
            }
            return SO;
        }
        public static void DoSomething()
        {
            try
            {
                Directory.CreateDirectory(RandomAlgorithmOutputPath);
                StreamWriter TestFile = new StreamWriter(String.Format(@"{0}\DoSomething.txt", RandomAlgorithmOutputPath), append: true);
                for(int i = 0; i < 1000; i++)
                {
                    TestFile.WriteLine(i);
                }
                TestFile.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
