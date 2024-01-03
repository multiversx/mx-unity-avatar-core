using System;
using System.IO;
using System.Linq;
using MultiversX.Avatar.Core.Operations.Managers;
using UnityEditor;

namespace MultiversX.Avatar.Core.Utilities
{
    public class Config
    {
        private const string TargetDir = "Assets";
        private const string SourceDir = "Packages/multiversx.avatar.core/Assets";
        private static readonly string[] ConfigFiles = { "NuGet.config", "packages.config", };

        public static void CopyConfigs(bool refreshAssets = true)
        {
            DirectoryUtility.CreateDirectory(TargetDir);

            foreach (string configFile in ConfigFiles)
            {
                FileUtility.CopyFile($"{SourceDir}/{configFile}", $"{TargetDir}/{configFile}");
            }

            if (refreshAssets)
            {
                AssetDatabase.Refresh();
            }
        }

        public static bool Validate()
        {
            return ConfigFiles.All(configFile => File.Exists($"{TargetDir}/{configFile}"));
        }

        public static void LoadConfig()
        {
            throw new NotImplementedException();
        }
    }
}
