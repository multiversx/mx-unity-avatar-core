namespace MultiversX.Avatar.Core.Operations
{
    public class InstallGltFastOperation : InstallPackageFromGitOperation<ProjectContext>
    {
        private const string PackageGitUrl = "https://github.com/atteneder/glTFast.git";
        private const string PackageID = "com.atteneder.gltfast";

        public InstallGltFastOperation()
            : base(PackageGitUrl, PackageID)
        {
            Name = Texts.InstallGltFast;
            Description = Texts.InstallingGltFast;
        }
    }
}
