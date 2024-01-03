namespace MultiversX.Avatar.Core.Operations.Managers
{
    public static class DirectoryUtility
    {
        public static bool CreateDirectory(string path)
        {
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);

            return true;
        }
    }
}
