namespace YoutubeDownloadUtility;

struct DownloadConfig
{
    public string YtdlpPath { get; set; }
    public string ArgsPath { get; set; }
    public string OutputDirectory { get; set; }
    public string PlaylistLink { get; set; }
}