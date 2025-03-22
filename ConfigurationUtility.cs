namespace YoutubeDownloadUtility;

static class ConfigurationUtility
{
    private const string CONFIG_NAME = "Config";

    public static Config LoadOrConfigure()
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

    public static Config ConfigureAndSave()
    {
        string path = GetConfigPath();

        return ConfigureAndSave(path);
    }

    private static Config Load(string path)
    {
        using Stream fs             = File.OpenRead(path);
        using BinaryReader reader   = new(fs);

        Config config = new();

        config.YtdlpPath            = reader.ReadString();
        config.DownloadArgsPath     = reader.ReadString();
        config.FFmpegPath           = reader.ReadString();

        return config;
    }

    private static Config Configure()
    {
        Console.WriteLine("[CUNFIGURE]");

        Config config = new();

        config.YtdlpPath            = Utils.GetExistedFile("Enter yt-dlp path");
        config.DownloadArgsPath     = Utils.GetExistedFile("Enter yt-dlp args path");
        config.FFmpegPath           = Utils.GetExistedFile("Enter FFmpeg path");

        return config;
    }

    private static Config ConfigureAndSave(string path)
    {
        Config config = Configure();

        Save(ref config, path);

        return config;
    }

    private static void Save(ref Config config, string path)
    {
        using Stream fs             = File.OpenWrite(path);
        using BinaryWriter writer   = new(fs);

        writer.Write(config.YtdlpPath);
        writer.Write(config.DownloadArgsPath);
        writer.Write(config.FFmpegPath);
    }
}