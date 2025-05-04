namespace YoutubeDownloadUtility;

internal class Program
{
    static void Main()
    {
    retry:
        PathConfig pathConfig           = GetPathConfig();
        DownloadConfig downloadConfig   = GetDownloadConfig(ref pathConfig);

        Console.WriteLine();
        Console.WriteLine("---Downloading---");
        Console.WriteLine();

        Downloader.Download(downloadConfig);

        Console.WriteLine();
        Console.WriteLine("---Converting---");
        Console.WriteLine();

        List<string> originalFiles = FileFinder.FindAll(downloadConfig.OutputDirectory,
                                                        ".m4a");

        AudioConverter.ConvertToMp3(pathConfig.FFmpegPath, originalFiles);

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

    private static PathConfig GetPathConfig()
    {
        PathConfig config = ConfigurationUtility.LoadOrConfigure();

        while (true)
        {
            config.Show();

            if (Utils.Confirm("Confirm?"))
                return config;
            else
                config = ConfigurationUtility.ConfigureAndSave();
        }
    }

    private static DownloadConfig GetDownloadConfig(ref PathConfig pathConfig)
    {
        DownloadConfig downloadConfig   = new();
        downloadConfig.PathConfig       = pathConfig;
        downloadConfig.OutputDirectory  = GetOutputDirectory();
        downloadConfig.PlaylistLink     = GetPlaylistLink();

        return downloadConfig;
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
    private static void Delete(List<string> oldFiles)
    {
        foreach (string file in oldFiles)
        {
            Console.WriteLine(file);

            File.Delete(file);
        }
    }
}