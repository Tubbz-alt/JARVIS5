using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    public static class JARVISRandomAlgorithms
    {
        public static string RandomAlgorithmOutputPath = @"C:\JARVIS5\RandomAlgorithmOutput";
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
                    DictionaryStorage.ExecuteInsertQuery(insertQuery);
                    Console.WriteLine(item);
                }
            }
            catch(Exception e)
            {
                SO = new StatusObject(StatusCode.FAILURE, "StringPermutationError", e.Message, e.ToString());
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
