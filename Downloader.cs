namespace YoutubeDownloadUtility;

static class Downloader
{
    public static void Download(DownloadConfig config)
    {
        PathConfig pathConfig = config.PathConfig;

        string args = GetArgs(pathConfig.YtdlpConfigPath, pathConfig.FFmpegPath, 
                              config.OutputDirectory, config.PlaylistLink);

        ProcessProxy.Run(pathConfig.YtdlpPath, args);
    }

    private static string GetArgs(string path, string ffmpegPath, string outputDirectory, 
                                  string playlistLink) 
        => $"--config-location \"{path}\" --ffmpeg-location \"{ffmpegPath}\" " +
           $"-P \"{outputDirectory}\" {playlistLink}";
}