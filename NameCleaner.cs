namespace YoutubeDownloadUtility;

static class NameCleaner
{
    public static void Clean(List<string> files)
    {
        for (int i = 0; i < files.Count; i++)
        {
            string file     = files[i];
            string newName  = TrimUselessPart(file);

            Console.WriteLine(file);

            if (!File.Exists(newName))
                File.Move(file, newName);

            files[i] = newName;
        }
    }

    private static string TrimUselessPart(string currentName)
    {
        int uselessInformationBegin = currentName.LastIndexOf('[');
        int uselessInformationEnd   = currentName.LastIndexOf(']');

        uselessInformationBegin--;
        uselessInformationEnd++;

        string uselessInformation = currentName[uselessInformationBegin..uselessInformationEnd];

        currentName = currentName.Replace(uselessInformation, "");
        currentName = currentName.Replace("_", " ");
        currentName = currentName.Replace("*", "");
        currentName = currentName.Replace("＂", "");

        return currentName.TrimEnd();
    }
}