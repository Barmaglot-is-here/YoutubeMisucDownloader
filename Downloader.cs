namespace YoutubeDownloadUtility;

static class Downloader
{
    public static void Download(DownloadConfig config)
    {
        string args = GetArgs(config.ArgsPath, config.OutputDirectory, config.PlaylistLink);

        ProcessProxy.Run(config.YtdlpPath, args);
    }

    private static string GetArgs(string path, string outputDirectory, string playlistLink)
    {
        string args = File.ReadAllText(path);

        return args + $" -P \"{outputDirectory}\" " + playlistLink;
    }
}