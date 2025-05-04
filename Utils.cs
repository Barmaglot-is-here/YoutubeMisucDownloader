namespace YoutubeDownloadUtility;

static class Utils
{
    public static string GetNotNullString(string message, string errorMessage)
    {
        while (true)
        {
            Console.WriteLine();
            Console.WriteLine(message);
            string? link = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(link))
                Console.WriteLine(errorMessage);
            else
                return link;
        }
    }

    public static string GetExistedFile(string message)
    {
        while (true)
        {
            string path = GetNotNullString(message, "Input is null");

            if (!File.Exists(path))
                Console.WriteLine("File doesn't exists");
            else
                return path;
        }
    }

    public static bool Confirm(string message)
    {
        Console.WriteLine($"{message} [Y/N]");

        while (true)
        {
            var key = Console.ReadKey();
            Console.WriteLine();

            if (key.Key == ConsoleKey.Y)
                return true;
            else if (key.Key != ConsoleKey.N)
                Console.WriteLine("Wrong key");
            else
                return false;
        }
    }
}