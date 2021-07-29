using System;
using System.Collections.Generic;
using System.Text;

namespace GamerScraper.Utilities
{
    class Utilities
    {
        //Returns true if the URL is valid, otherwise return false
        public static bool CheckURL(string URL)
        {
            if (Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                Uri UriURL = new Uri(URL);
                if (UriURL.Scheme != Uri.UriSchemeHttp && UriURL.Scheme != Uri.UriSchemeHttps)
                {
                    return false;
                }
                return true;
            }
            return false;
        }
        public static void ColorfulWriteLine(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}