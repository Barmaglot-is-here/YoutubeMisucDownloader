namespace YoutubeDownloadUtility;

struct PathConfig
{
    public string YtdlpPath { get; set; }
    public string YtdlpConfigPath { get; set; }
    public string FFmpegPath { get; set; }

    public void Show()
    {
        Console.WriteLine();
        Console.WriteLine("[CONFIGURATION]");
        Console.WriteLine($"Yt-dlp path          |  {YtdlpPath}");
        Console.WriteLine($"Yt-dlp config path   |  {YtdlpConfigPath}");
        Console.WriteLine($"FFmpeg path          |  {FFmpegPath}");
        Console.WriteLine();
    }
}