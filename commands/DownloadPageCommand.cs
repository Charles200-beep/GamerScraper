using CommandLine;
using GamerScraper;
using GamerScraper.Utilities;
using System;
using System.IO;
using System.Net;

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
            if (!Utilities.CheckURL(URL))
            {
                Utilities.ColorfulWriteLine("Bad URL!", ConsoleColor.Red);
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
    }
}

