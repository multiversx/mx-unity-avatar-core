namespace MultiversX.Avatar.Core.Operations
{
    public class InstallModulesOperation : InstallPackageFromGitOperation<ProjectContext>
    {
        private static readonly string PackageGitUrl = Constants.ModuleGitUrl;
        private const string PackageID = "multiversx.avatar.loader";

        public InstallModulesOperation()
            : base(PackageGitUrl, PackageID)
        {
            Name = Texts.InstallModules;
            Description = Texts.InstallingModules;
        }
    }
}
