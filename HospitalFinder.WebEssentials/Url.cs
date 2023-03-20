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

        public static string GenerateGoogleMapsLink(double latitude, double longtitude)
        {
            string convertedLatitude = Coordinate.Convert.ToDMS(latitude).Replace(" ", "") + (latitude >= 0 ? "N" : "S");
            string convertedLongtitude = Coordinate.Convert.ToDMS(longtitude).Replace(" ", "") + (longtitude >= 0 ? "E" : "W");

            string googleMapsLink = $"https://www.google.com/maps/place/{convertedLatitude}+{convertedLongtitude}";
            return googleMapsLink;
        }
    }
}
