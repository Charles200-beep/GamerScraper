using System;
using System.IO;
using System.Net;
using CommandLine;
using GamerScraper;

namespace DownloadFileCommandFile
{
    [Verb("downloadfile", HelpText = "Download a file")]
    public class DownloadFileCommand : ICommand
    {
        [Option('u', "URL", Required = true, HelpText = "Allows for a URL to be entered")]
        public string URL { get; set; }

        [Option('p', "path", Required = true, HelpText = "Allows for a path to be entered")]
        public string path { get; set; }

        public void Execute()
        {
            DownloadFile();
        }

        private void DownloadFile()
        {
            Uri UriURL = new Uri(URL);

            //Checks if the URL is well formed, then if it's in an HTTPS or HTTP format, then checks if it's a file URL, then if the path exists. (1000% a better way to do this)
            if (Uri.IsWellFormedUriString(URL, UriKind.Absolute))
            {
                if (UriURL.Scheme != Uri.UriSchemeHttp && UriURL.Scheme != Uri.UriSchemeHttps)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Invalid URL! (The address is neither an HTTP or an HTTPS address.)");
                    Console.ResetColor();
                }
                else
                {
                    if (Path.HasExtension(URL))
                    {
                        if (Directory.Exists(path))
                        {
                            //Adds a "\" to the path if it's absent
                            string fullPath;
                            if (path.EndsWith(@"\"))
                            {
                                fullPath = path;
                            }
                            else
                            {
                                fullPath = path + @"\";
                            }

                            //This is for determining the name of the file/the extension of the file
                            string file = Path.GetFileName(UriURL.AbsolutePath);

                            using (var client = new WebClient())
                            {
                                client.DownloadFile(URL, fullPath + file);
                            }

                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid Path!");
                            Console.ResetColor();
                        }
                    } 
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid URL! (The address is not a file address.)");
                        Console.ResetColor();
                    }
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid URL!");
                Console.ResetColor();
            }
        }

    }
}