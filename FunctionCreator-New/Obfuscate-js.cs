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

            obfuscated.Append("eval(\"");
            foreach(string hex in tmp)
            {
                obfuscated.Append($@"\x{hex}");
            }
            obfuscated.Append("\");");

            return obfuscated.ToString();
        }
    }
}
