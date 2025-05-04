namespace YoutubeDownloadUtility;

static class ConfigurationUtility
{
    private const string CONFIG_NAME = "Config";

    public static PathConfig LoadOrConfigure()
    {
        string path = GetConfigPath();

        if (File.Exists(path))
            return Load(path);
        else
        {
            Console.WriteLine("Config is null\n");

            return ConfigureAndSave(path);
        }
    }

    private static string GetConfigPath() 
        => Environment.CurrentDirectory + "\\" + CONFIG_NAME + ".bin";

    public static PathConfig ConfigureAndSave()
    {
        string path = GetConfigPath();

        return ConfigureAndSave(path);
    }

    private static PathConfig Load(string path)
    {
        using Stream fs             = File.OpenRead(path);
        using BinaryReader reader   = new(fs);

        PathConfig config = new();

        config.YtdlpPath            = reader.ReadString();
        config.YtdlpConfigPath     = reader.ReadString();
        config.FFmpegPath           = reader.ReadString();

        return config;
    }

    private static PathConfig Configure()
    {
        Console.WriteLine("[CUNFIGURE]");

        PathConfig config = new();

        config.YtdlpPath            = Utils.GetExistedFile("Enter yt-dlp path");
        config.YtdlpConfigPath     = Utils.GetExistedFile("Enter yt-dlp config path");
        config.FFmpegPath           = Utils.GetExistedFile("Enter FFmpeg path");

        return config;
    }

    private static PathConfig ConfigureAndSave(string path)
    {
        PathConfig config = Configure();

        Save(ref config, path);

        return config;
    }

    private static void Save(ref PathConfig config, string path)
    {
        using Stream fs             = File.OpenWrite(path);
        using BinaryWriter writer   = new(fs);

        writer.Write(config.YtdlpPath);
        writer.Write(config.YtdlpConfigPath);
        writer.Write(config.FFmpegPath);
    }
}