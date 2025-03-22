using System.Diagnostics;

namespace YoutubeDownloadUtility;

static class ProcessProxy
{
    public static void Run(string fileName, string args)
    {
        Process process             = new();
        ProcessStartInfo startInfo  = process.StartInfo;

        startInfo.FileName  = fileName;
        startInfo.Arguments = args;

        process.Start();

        while (!process.HasExited) ;

        process.Dispose();
    }
}