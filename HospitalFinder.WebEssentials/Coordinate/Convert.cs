using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalFinder.WebEssentials.Coordinate
{
    public static class Convert
    {
        /// <summary>
        /// Converts a coordinate in DMS format to a single floating point number
        /// </summary>
        /// <param name="coordinate"> coordinate in Degree Minute Second format </param>
        /// <returns> The coordinate converted to a decimal value of type double </returns>
        public static double ToDecimal(string coordinate)
        {
            // Remove all the spaces, degree signs, single and double qoutation marks
            coordinate = coordinate.Replace(" ", "")
                .Replace("'", "")
                .Replace("\"", "");


            // Hold the first character of the coordinates which is likey to be the sign ( - / + )
            char sign = coordinate[0];

            // The index of the degree sign
            int degreeSignIndex = coordinate.IndexOf('°');

            // Variables to hold the value of degrees, minutes and seconds after processing the coordinate string
            double degree = 0;
            double min = 0;
            double sec = 0;

            // If the coordinate has a sign (positive or negative)...
            if (sign == '-' || sign == '+')
            {
                // Parse the first 3 or 4 characters (depending on the index of the degree sign) into
                // a floating point number (the sign is included)
                degree = double.Parse(coordinate.Substring(0, degreeSignIndex));

                // Concatinate the sign of the coordinate to the 5th and 6th or 6th and 7th characters
                // (depending on the index of the degree sign) of it, parse it into a floating point number
                // and save it in the min variable
                min = double.Parse(sign + (coordinate.Substring(degreeSignIndex + 1, 2)));

                // Concatinate the sign of the coordinate to the 7th or 8th (depending on the index of the degree sign)
                // and the remaining characters of it, parse it into a floating point number and save it in the min variable
                sec = double.Parse(sign + (coordinate.Substring(degreeSignIndex + 3)));
            }

            // Otherwise... 
            else
            {
                // Parse the first 2 or 3 characters into a floating point number and save it in degree variable
                degree = double.Parse(coordinate.Substring(0, degreeSignIndex));

                // Parse the 3rd and 4th or 4th and 5th  (depending on the index of the degree sign) characters
                // into a floating point number and save it in min variable
                min = double.Parse(coordinate.Substring(degreeSignIndex + 1, 2));

                // Parse the 6rd or 7th  (depending on the index of the degree sign) and the remaining characters
                // into a floating point number and save it in sec variable
                sec = double.Parse(coordinate.Substring(degreeSignIndex + 3));
            }

            // Return the calculated and converted value of the coordinate with 5 digits after the decimal
            return Math.Round(degree + (min / 60) + (sec / 3600), 5);
        }

        public static string ToDMS(double coordinate)
        {
            // Variables to hold the value of degrees, minutes and seconds after processing
            // the coordinate floating point number
            double degree;
            double min;
            double sec;

            // The digits of the coordinate before the decimals is the degree, but because those digits
            // are needed for the next processes, the value stays equal to coordinate
            degree = coordinate;

            // The value of min is equal to the whole part of the number multiplied by 60 
            min = ((degree < 0) ? (Math.Ceiling(degree) - degree) : (degree - Math.Floor(degree))) * 60;

            // The value of sec is the decimal numbers of the degree multiplied by 60 
            sec = (min - Math.Floor(min)) * 60;


            // The decimal part of the number for minute is removed
            min = Math.Floor(min);

            // Except for 2 of the decimal digits, the rest of removed and the number is rounded
            sec = Math.Round(sec, 2);


            // The degrees, minutes and seconds are are displayed in the correct format
            return $"{degree:0}° {min}' {sec}\"";
        }
    }
}
