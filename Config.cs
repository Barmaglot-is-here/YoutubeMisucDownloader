namespace YoutubeDownloadUtility;

struct Config
{
    public string YtdlpPath { get; set; }
    public string DownloadArgsPath { get; set; }
    public string FFmpegPath { get; set; }

    public void Show()
    {
        Console.WriteLine();
        Console.WriteLine("[CONFIGURATION]");
        Console.WriteLine($"Yt-dlp path          |  {YtdlpPath}");
        Console.WriteLine($"Download args path   |  {DownloadArgsPath}");
        Console.WriteLine($"FFmpeg path          |  {FFmpegPath}");
        Console.WriteLine();
    }
}