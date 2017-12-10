using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionCreator_New
{
    public class Obfuscate_js
    {
        public static string Obfuscate(string before)
        {
            var data = Encoding.UTF8.GetBytes(before);
            var tmp = BitConverter.ToString(data).Split('-');

            StringBuilder obfuscated = new StringBuilder(); //難読化後
            var rand = new Random();
            var hexs = new List<string>();
            for(int i = 0; i < 8; i++)
            {
                int ascii = rand.Next(16, 128);
                hexs.Add($"\"\\x{Convert.ToString(ascii, 16)}\"");
            }

            obfuscated.Append("(function (f,u,n,c,t,i,o,n){eval(\"");
            foreach(string hex in tmp)
            {
                obfuscated.Append($@"\x{hex}");
            }
            obfuscated.Append($"\");}}({string.Join(",",hexs)}));");

            return obfuscated.ToString();
        }
    }
}
