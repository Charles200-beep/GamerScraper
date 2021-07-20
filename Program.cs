using System;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using CommandLine;
namespace GamerScraper
{
    public interface ICommand
    {
        void Execute();
    }

    [Verb("download", HelpText = "Download a webpage")]
    public class DownloadCommand : ICommand
    {
        [Option('u', "URL", Required = true, HelpText = "Allows for a URL to be entered")]
        public string URL { get; set; }
        [Option('p', "path", Required = true, HelpText = "Path of the file")]
        public string path { get; set; }
        public void Execute()
        {
            DownloadPage(URL);
        }

        public void DownloadPage(string URL)
        {
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
                        string fullPath;
                        if (path.EndsWith(@"\"))
                        {
                            fullPath = path + "page.html";
                        }
                        else
                        {
                            fullPath = path + @"\page.html";
                        }

                        WebClient client = new WebClient();
                        try
                        {
                            client.DownloadFile(URL, fullPath);
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
                    } else
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


    [Verb("test", HelpText = "test command")]
    public class TestCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("test testing test");
        }
    }


    class Program
    {

        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<DownloadCommand, TestCommand>(args)
                    .WithParsed<ICommand>(t => t.Execute());
        }

    }
}
