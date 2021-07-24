using System;
using System.IO;
using System.Net;
using CommandLine;
using GamerScraper;

namespace DownloadPageCommandFile
{
    [Verb("downloadpage", HelpText = "Download a webpage")]
    public class DownloadPageCommand : ICommand
    {
        [Option('u', "URL", Required = true, HelpText = "Allows for a URL to be entered")]
        public string URL { get; set; }
        [Option('p', "path", Required = true, HelpText = "Path of the file")]
        public string path { get; set; }
        public void Execute()
        {
            DownloadPage();
        }

        private void DownloadPage()
        {
            //Checks if the URL is well formed, then if it's in an HTTPS or HTTP format, then if the path exists.
            if (Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                Uri UriURL = new Uri(URL);
                if (UriURL.Scheme != Uri.UriSchemeHttp && UriURL.Scheme != Uri.UriSchemeHttps)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid URL! (The address is neither an HTTP or an HTTPS address.)");
                    Console.ResetColor();
                }
                else
                {
                    if (Directory.Exists(path))
                    {
                        //Adds a "\" to the path if it's absent
                        string fullPath;
                        if (path.EndsWith(@"\"))
                        {
                            fullPath = path + "page.html";
                        }
                        else
                        {
                            fullPath = path + @"\page.html";
                        }

                        try
                        {
                            using (var client = new WebClient())
                            {
                                client.DownloadFile(URL, fullPath);
                            }
                        }
                        catch (WebException ex)
                        {
                            Console.WriteLine("There was an error while trying to access the webpage!");
                            Console.WriteLine("Exception: " + ex);
                        }
                        catch (ArgumentNullException ex)
                        {
                            Console.WriteLine("There was an error!");
                            Console.WriteLine("Exception: " + ex);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Path!");
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid URL!");
                Console.Write("Have you entered the complete URL (e.g https://www.google.com/)?");
                Console.ResetColor();
            }
        }

    }

}