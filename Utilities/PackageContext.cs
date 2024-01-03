namespace MultiversX.Avatar.Core.Utilities
{
    public class PackageContext : Context
    {
        public const string CorePackageId = "multiversx.avatar.core";
        public static bool LocalMode = false;
        public Package[] Dependencies { get; set; }
    }
}
