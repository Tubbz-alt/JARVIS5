using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JARVIS5
{
    public static class JARVISUniversalDefinitions
    {
        public static Dictionary<char, string> BatchFileEscapeCharacters = new Dictionary<char, string>()
        {
                {'%', "%%"}, //37
                {'^', "^^"}, //94
                {'&', "^&"}, //38
                {'>', "^>"}, //62
                {'<', "^<"}, //60
                {'|', "^|"}, //124
                {'\'', "^'"}, //39
                {'`', "^`"},//96
                {',', "^,"},//44
                {';', "^;"},//59
                {'=', "^="},
                {'(', "^("},
                {')', "^)"},
                {'!', "!"},
                {'\"', "\\\""},
                {'\\', "\\\\"},
                {'[', "["},
                {']', "]"},
                {'.', "."},
                {'*', "*"},
                {'?', "?" }
        };
        public static Dictionary<string, string> SqlServerHashTypes = new Dictionary<string, string>()
        {
            { "MD5","32" },
            { "SHA","40" },
            { "SHA1","40" },
            { "SHA2_256","64" },
            { "SHA2_512","128" }
        };
    }
}
