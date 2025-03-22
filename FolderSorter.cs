namespace YoutubeDownloadUtility;

static class FolderSorter
{
    public static void Sort(List<string> files)
    {
        while (files.Count > 0)
        {
            string file = files[0];
            int folderNameIndex = file.IndexOf("-");

            if (folderNameIndex < 1)
            {
                files.Remove(file);

                continue;
            }

            string folderName = file[..folderNameIndex];
            folderName = folderName.TrimEnd();

            var sameFiles = FindTheSame(folderName, files);

            if (sameFiles.Count > 1)
                MoveGroup(folderName, sameFiles);
        }
    }

    private static Queue<string> FindTheSame(string folderName, List<string> files)
    {
        Queue<string> sameFiles = new();

        int counter = 0;

        while (counter < files.Count)
        {
            string file = files[counter];

            if (file.StartsWith(folderName))
            {
                files.Remove(file);

                sameFiles.Enqueue(file);
            }
            else
                counter++;
        }

        return sameFiles;
    }

    private static void MoveGroup(string folderName, IEnumerable<string> files)
    {
        Directory.CreateDirectory(folderName);

        foreach (string originalPath in files)
        {
            string fileName = originalPath.Replace(folderName, "");
            fileName = fileName.TrimStart().TrimStart('-');

            string destinationPath = folderName + "\\" + fileName;

            try
            {
                File.Move(originalPath, destinationPath);
            }
            catch
            {
                Console.WriteLine("Folder: " + folderName);
                Console.WriteLine("Original: " + originalPath);
                Console.WriteLine("Result: " + destinationPath);

                throw;
            }
        }
    }
}