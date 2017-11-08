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
                string TableQuery =
                    @"CREATE TABLE[dbo].[RAINBOW_{0}](" +
                        "[ID][bigint] IDENTITY(1, 1) NOT NULL," +
                        "[Word] [varchar] (255) NOT NULL," +
                        "[FirstLetter] [varchar] (1) NOT NULL," +
                        "[LetterCount] [int] NOT NULL," +
                        "[MD5] [varchar] (255) NULL," +
                        "[SHA] [varchar] (255) NULL," +
                        "[SHA1] [varchar] (255) NULL," +
                        "[SHA2_256] [varchar] (255) NULL," +
                        "[SHA2_512] [varchar] (255) NULL," +
                     "CONSTRAINT[PK_RAINBOW_{0}] PRIMARY KEY CLUSTERED" +
                    "(" +
                       "[ID] ASC" +
                    ")WITH(PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON[PRIMARY]" +
                    ") ON[PRIMARY]";
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
                    StatusObject SO_CreateTable = DictionaryStorage.ExecuteNonReaderQuery(String.Format(TableQuery, (int)Character));
                    Console.WriteLine("Creating Table RAINBOW_{0}", (int)Character);
                    if (SO_CreateTable.Status == StatusCode.FAILURE)
                    {
                        Console.WriteLine(SO_CreateTable.ErrorMessage);
                    }
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
            }
            return SO;
        }
        public static StatusObject PopulateStringPermutationTable(string WordLength, char FirstLetter, JARVISDataSource DictionaryStorage)
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
                ReorderedString += FirstLetter;
                foreach (char TargetCharacter in TargetString)
                {
                    if(TargetCharacter != FirstLetter)
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
                    if(item[0] == FirstLetter)
                    {
                        string insertQuery = String.Format("insert into RAINBOW_{3} values ('{0}','{1}',{2})", item, item[0], item.Length, (int)item[0]);
                        Console.WriteLine(item);
                        /*StatusObject SO_AddRecord = DictionaryStorage.ExecuteNonReaderQuery(insertQuery);
                        if (SO_AddRecord.Status == StatusCode.FAILURE)
                        {
                            Console.WriteLine(SO_AddRecord.ErrorMessage);
                        }*/
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
        public static StatusObject CreateStringPermutationBatchFiles(string MaxWordLength)
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
                                "{2} wordlist populatetables {0} {1}",
                                (requiresEscape) ? ReplacementCharacter : TargetCharacter.ToString(),
                                i,
                                System.Reflection.Assembly.GetEntryAssembly().Location);
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
