namespace YoutubeMisucDownloader;
internal static class Extensions
{
    public static void WaitAndDispose(this IEnumerable<Task> tasks)
    {
        foreach (var task in tasks)
        {
            task.Wait();
            task.Dispose();
        }
    }
}