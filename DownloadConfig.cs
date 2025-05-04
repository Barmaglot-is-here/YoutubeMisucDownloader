namespace YoutubeDownloadUtility;

struct DownloadConfig
{
    public PathConfig PathConfig { get; set; }
    public string OutputDirectory { get; set; }
    public string PlaylistLink { get; set; }
}