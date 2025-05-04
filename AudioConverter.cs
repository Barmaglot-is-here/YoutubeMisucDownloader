using System.Collections.Concurrent;
using YoutubeMisucDownloader;

namespace YoutubeDownloadUtility;

static class AudioConverter
{
    public static void ConvertToMp3(string FFmpegPath, IEnumerable<string> musicFiles)
    {
        Queue<Task> tasks                   = new();
        ConcurrentQueue<string> fileQueue   = new(musicFiles);

        int count = fileQueue.Count;

        for (int i = 0; i < count; i++)
        {
            Task task = Task.Run(() =>
            {
                string path;
                while (!fileQueue.TryDequeue(out path!)) ;

                ConvertFile(FFmpegPath, path);
            });

            tasks.Enqueue(task);
        }

        tasks.WaitAndDispose();
    }

    private static void ConvertFile(string FFmpegPath, string path)
    {
        string originalExtension    = Path.GetExtension(path);
        string soundPath            = path.Replace(originalExtension, ".mp3");
        string coverPath            = path.Replace(originalExtension, ".png");

        string args1 = ExtractPng(path, coverPath);
        string args2 = Convert(path, soundPath);
        string args3 = SetCover(coverPath, soundPath);

        ProcessProxy.Run(FFmpegPath, args1);
        ProcessProxy.Run(FFmpegPath, args2);
        ProcessProxy.Run(FFmpegPath, args3);
    }

    private static string ExtractPng(string soundPath, string newPath)
        => $"-i \"{soundPath}\" -an -c:v copy \"{newPath}\"";

    private static string Convert(string originalPath, string newPath)
        => $"-i \"{originalPath}\" \"{newPath}\"";

    private static string SetCover(string coverPath, string filePath)
        => $"-i \"{filePath}\" \"{coverPath}\" -c copy -map 0 -map 1 -metadata:s:v title=\"Album cover\" \"{filePath}\"_.mp3";
}