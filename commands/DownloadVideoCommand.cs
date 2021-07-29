using CommandLine;
using GamerScraper;
using GamerScraper.Utilities;
using System;
using System.IO;
using VideoLibrary;

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