using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.WebEssentials
{
    public class Url
    {
        private static List<char> UnwiseOrReservedChars = new List<char>
        {
            ' ' ,'{' , '}' , '|' , '\\' , '^' , '[' , ']' , '`', '<', '>', ';' , '/' , '?' , ':' , '@' , '&' , '=' , '+' , '$' , ',', '.'
        };

        public static string CreateUrlFriendlyString(string title)
        {
            foreach (var character in UnwiseOrReservedChars)
            {
                title = title.Replace(character, '-');
            }
            return title;
        }
    }
}
