using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    public static class JARVISUniversalDefinitions
    {
        public static Dictionary<char,string> GetBatchFileEscapeCharacters()
        {
            Dictionary<char, string> BatchFileEscapeCharacters = new Dictionary<char, string>();
            try
            {
                BatchFileEscapeCharacters.Add('%', "%%");
                BatchFileEscapeCharacters.Add('^', "^^");
                BatchFileEscapeCharacters.Add('&', "^&");
                BatchFileEscapeCharacters.Add('>', "^>");
                BatchFileEscapeCharacters.Add('<', "^<");
                BatchFileEscapeCharacters.Add('|', "^|");
                BatchFileEscapeCharacters.Add('\'', "^'");
                BatchFileEscapeCharacters.Add('`', "^`");
                BatchFileEscapeCharacters.Add(',', "^,");
                BatchFileEscapeCharacters.Add(';', "^;");
                BatchFileEscapeCharacters.Add('=', "^=");
                BatchFileEscapeCharacters.Add('(', "^(");
                BatchFileEscapeCharacters.Add(')', "^)");
                BatchFileEscapeCharacters.Add('!', "^^!");
                BatchFileEscapeCharacters.Add('\"', "\"\"");
                BatchFileEscapeCharacters.Add('\\', "\\\\");
                BatchFileEscapeCharacters.Add('[', "\\[");
                BatchFileEscapeCharacters.Add(']', "\\]");
                BatchFileEscapeCharacters.Add('.', "\\.");
                BatchFileEscapeCharacters.Add('*', "\\*");
                BatchFileEscapeCharacters.Add('?', "\\?");
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                BatchFileEscapeCharacters = null;
            }
            return BatchFileEscapeCharacters;
        }
        public static Dictionary<char, string> BatchFileEscapeCharacters = new Dictionary<char, string>()
        {
                {'%', "%%"},
                {'^', "^^"},
                {'&', "^&"},
                {'>', "^>"},
                {'<', "^<"},
                {'|', "^|"},
                {'\'', "^'"},
                {'`', "^`"},
                {',', "^,"},
                {';', "^;"},
                {'=', "^="},
                {'(', "^("},
                {')', "^)"},
                {'!', "^^!"},
                {'\"', "\"\""},
                {'\\', "\\\\"},
                {'[', "\\["},
                {']', "\\]"},
                {'.', "\\."},
                {'*', "\\*"},
                {'?', "\\?" }
        };
    }
}
