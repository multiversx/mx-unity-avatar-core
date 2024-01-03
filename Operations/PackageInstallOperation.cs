using System.Threading;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Utilities;

namespace MultiversX.Avatar.Core.Operations
{
    public class PackageInstallOperation<TContext, TPackage> : Operation<TContext>
        where TContext : Context
        where TPackage : Package
    {
        private readonly TPackage _package;

        public PackageInstallOperation(TPackage package)
        {
            _package = package;
        }

        public override async Task<TContext> Execute(
            TContext context,
            CancellationToken cancellationToken
        )
        {
            CurrentStatus = IOperation<TContext>.Status.InProgress;
            await PackageHelpers.InstallPackageAsync(_package);
            CurrentStatus = IOperation<TContext>.Status.Complete;
            return context;
        }
    }
}
