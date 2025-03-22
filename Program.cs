namespace YoutubeDownloadUtility;

internal class Program
{
    static void Main()
    {
    retry:
        Config config = GetConfig();

        DownloadConfig downloadConfig   = From(ref config);
        downloadConfig.OutputDirectory  = GetOutputDirectory();
        downloadConfig.PlaylistLink     = GetPlaylistLink();

        Console.WriteLine();
        Console.WriteLine("---Downloading---");
        Console.WriteLine();

        Downloader.Download(downloadConfig);

        Console.WriteLine();
        Console.WriteLine("---Converting---");
        Console.WriteLine();

        List<string> originalFiles = FileFinder.FindAll(downloadConfig.OutputDirectory, ".m4a");

        AudioConvertor.ConvertToMp3(config.FFmpegPath, originalFiles);

        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine();
        Console.WriteLine("---Postprocessing---");
        Console.WriteLine();

        List<string> mp3Files = FileFinder.FindAll(downloadConfig.OutputDirectory, ".mp3");

        NameCleaner.Clean(mp3Files);

        Console.WriteLine();
        Console.WriteLine("---Sorting---");
        Console.WriteLine();

        FolderSorter.Sort(mp3Files);

        Console.WriteLine();
        Console.WriteLine("---Cleaning---");
        Console.WriteLine();

        Delete(originalFiles);

        if (Utils.Confirm("Done. Retry?"))
            goto retry;
    }

    private static Config GetConfig()
    {
        Config config = ConfigurationUtility.LoadOrConfigure();

        while (true)
        {
            config.Show();

            if (Utils.Confirm("Continue?"))
                return config;
            else
                config = ConfigurationUtility.ConfigureAndSave();
        }
    }

    private static string GetOutputDirectory()
    {
        string outputDirectory = Utils.GetNotNullString("Enter output directory",
                                                        "Output directory is null");

        if (!Directory.Exists(outputDirectory))
            Directory.CreateDirectory(outputDirectory);

        return outputDirectory;
    }

    private static string GetPlaylistLink() => Utils.GetNotNullString("Enter playlist link",
                                                                      "Link is null");

    private static DownloadConfig From(ref Config config)
    {
        DownloadConfig downloadConfig   = new();
        downloadConfig.YtdlpPath        = config.YtdlpPath;
        downloadConfig.ArgsPath         = config.DownloadArgsPath;

        return downloadConfig;
    }

    private static void Delete(List<string> oldFiles)
    {
        foreach (string file in oldFiles)
        {
            Console.WriteLine(file);

            File.Delete(file);
        }
    }
}