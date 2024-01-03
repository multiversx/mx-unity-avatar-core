using MultiversX.Avatar.Core.Operations;

namespace MultiversX.Avatar.Core
{
    public class ProjectContext : Context
    {
        private static ProjectContext _instance;
        public static ProjectContext Instance
        {
            get { return _instance ??= new ProjectContext(); }
            set => _instance = value;
        }

        private ProjectContext() { }

        private static readonly IOperation<ProjectContext> CopyConfigsOperation =
            new CopyConfigsOperation<ProjectContext>();
        private static readonly IOperation<ProjectContext> InstallNuGetOperation =
            new InstallNuGetOperation();
        private static readonly IOperation<ProjectContext> InstallGltFastOperation =
            new InstallGltFastOperation();
        private static readonly IOperation<ProjectContext> InstallModulesOperation =
            new InstallModulesOperation();

        public static readonly IOperation<ProjectContext>[] ConfigureOperations =
        {
            CopyConfigsOperation,
            InstallNuGetOperation,
            InstallGltFastOperation,
            InstallModulesOperation,
        };

        public static bool ForceInstall = false;
    }
}
