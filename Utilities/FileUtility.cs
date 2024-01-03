using UnityEditor;

namespace MultiversX.Avatar.Core
{
    public static class FileUtility
    {
        public static bool CopyFile(string sourcePath, string targetPath)
        {
            if (!System.IO.File.Exists(targetPath))
            {
                System.IO.File.Copy(sourcePath, targetPath);
                return true;
            }

            bool response = EditorUtility.DisplayDialog(
                "File Already Exists",
                $"The file {targetPath} already exists. Do you want to overwrite it?",
                "Yes",
                "No"
            );

            if (!response)
                return false;

            System.IO.File.Copy(sourcePath, targetPath, true);
            return true;
        }
    }
}
