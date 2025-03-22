using System.Collections.Concurrent;

namespace YoutubeDownloadUtility;

static class AudioConvertor
{
    public static void ConvertToMp3(string FFmpegPath, IEnumerable<string> musicFiles)
    {
        List<Task> tasks                    = new();
        ConcurrentQueue<string> original    = new(musicFiles);

        int count = original.Count;

        for (int i = 0; i < count; i++)
        {
            Task task = Task.Run(() =>
            {
                string path;
                while (!original.TryDequeue(out path!)) ;

                ConvertFile(FFmpegPath, path);
            });

            tasks.Add(task);
        }

        while(tasks.Count > 0)
            for (int i = 0; i < tasks.Count;)
            {
                Task task = tasks[i];

                if (task.IsCompleted)
                {
                    if (task.IsFaulted)
                        throw task.Exception;

                    task.Dispose();
                    tasks.Remove(task);
                }
            }
    }

    private static void ConvertFile(string FFmpegPath, string path)
    {
        string originalExtension    = Path.GetExtension(path);
        string newPath              = path.Replace(originalExtension, ".mp3");

        string args = GetArgs(path, newPath);

        ProcessProxy.Run(FFmpegPath, args);
    }

    private static string GetArgs(string soundPath, string newPath) 
        => "-i " + $"\"{soundPath}\"" + " " + $"\"{newPath}\"";
}