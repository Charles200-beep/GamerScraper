using CommandLine;
using DownloadPageCommandFile;
using DownloadVideoCommandFile;
using DownloadFileCommandFile;

namespace GamerScraper
{
    public interface ICommand
    {
        void Execute();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<DownloadVideoCommand, DownloadPageCommand, DownloadFileCommand>(args)
                    .WithParsed<ICommand>(t => t.Execute());
        }

    }
}
