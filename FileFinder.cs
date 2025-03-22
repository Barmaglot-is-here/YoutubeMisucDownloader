namespace YoutubeDownloadUtility;

static class FileFinder
{
    public static List<string> FindAll(string directory, string extension)
    {
        List<string> files = new();

        GetMusicFiles(files, directory, extension);

        return files;
    }

    private static void GetMusicFiles(List<string> writeIn, string directory, string extension)
    {
        var files = Directory.EnumerateFiles(directory, '*' + extension);

        writeIn.AddRange(files);

        foreach (var subdir in Directory.EnumerateDirectories(directory))
            GetMusicFiles(writeIn, subdir, extension);
    }
}