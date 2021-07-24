using System;
using System.IO;
using VideoLibrary;
using GamerScraper;
using CommandLine;
using System.Linq;

namespace DownloadVideoCommandFile
{
    [Verb("downloadvideo", HelpText = "Download a video")]
    public class DownloadVideoCommand : ICommand
    {
        [Option('u', "URL", Required = true, HelpText = "Allows for a URL to be entered")]
        public string URL { get; set; }

        [Option('p', "path", Required = true, HelpText = "Allows for a path to be entered")]
        public string path { get; set; }

        public void Execute()
        {
            DownloadVideo();
        }

        private void DownloadVideo()
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
                            fullPath = path;
                        }
                        else
                        {
                            fullPath = path + @"\";
                        }

                        var youTube = YouTube.Default;
                        var video = youTube.GetVideo(URL);
                        File.WriteAllBytes(fullPath + video.FullName, video.GetBytes());
                    }
                    else
                    {
                        Console.WriteLine("Invalid Path!");
                    }
                }
            }
        }
    }

}