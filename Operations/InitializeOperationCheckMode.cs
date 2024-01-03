using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Utilities;
using UnityEditor.PackageManager;

namespace MultiversX.Avatar.Core.Operations
{
    public class InitializeOperationCheckMode : Operation<PackageContext>
    {
        public override async Task<PackageContext> Execute(
            PackageContext context,
            CancellationToken cancellationToken
        )
        {
            CurrentStatus = IOperation<PackageContext>.Status.InProgress;
            PackageInfo[] installedPackages = await PackageHelpers.GetInstalledPackagesAsync();
            PackageInfo[] corePackage = installedPackages
                .Where(packageInfo => packageInfo.name == PackageContext.CorePackageId)
                .ToArray();
            PackageContext.LocalMode = corePackage.Any(
                packageInfo => packageInfo.source == PackageSource.Local
            );
            CurrentStatus = IOperation<PackageContext>.Status.Complete;
            return context;
        }
    }
}
