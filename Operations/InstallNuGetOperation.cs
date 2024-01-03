namespace MultiversX.Avatar.Core.Operations
{
    public class InstallNuGetOperation : InstallPackageFromGitOperation<ProjectContext>
    {
        private const string PackageGitUrl =
            "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity";
        private const string PackageId = "com.github-glitchenzo.nugetforunity";

        public InstallNuGetOperation()
            : base(PackageGitUrl, PackageId)
        {
            Name = Texts.InstallNuget;
            Description = Texts.InstallingNuget;
        }
    }
}
