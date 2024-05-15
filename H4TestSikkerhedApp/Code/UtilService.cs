namespace H4TestSikkerhedApp.Code
{
    public static class UtilService
    {
        public static string GetWorkingDir()
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "files");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            return Path.Combine(directoryPath, "lastrun.txt");
        }
    }
}
