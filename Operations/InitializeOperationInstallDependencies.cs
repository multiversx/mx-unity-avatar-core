using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Operations.Managers;
using MultiversX.Avatar.Core.Utilities;

namespace MultiversX.Avatar.Core.Operations
{
    public class InitializeOperationInstallDependencies : Operation<PackageContext>
    {
        private static readonly Package[] Dependencies =
        {
            new Package
            {
                Id = "com.atteneder.gltfast",
                GitUrl = "https://github.com/atteneder/glTFast.git",
            },
            new Package
            {
                Id = "com.github-glitchenzo.nugetforunity",
                GitUrl = "https://github.com/GlitchEnzo/NuGetForUnity.git?path=/src/NuGetForUnity",
            },
        };

        public override async Task<PackageContext> Execute(
            PackageContext context,
            CancellationToken cancellationToken
        )
        {
            IOperation<PackageContext>[] installOperations = { };

            CurrentStatus = IOperation<PackageContext>.Status.InProgress;

            Package[] missingDependencies = await PackageHelpers.GetMissingPackagesAsync(
                Dependencies
            );

            if (missingDependencies.Length > 0)
            {
                installOperations = missingDependencies.Aggregate(
                    installOperations,
                    (current, missingDependency) =>
                        current
                            .Append(
                                new PackageInstallOperation<PackageContext, Package>(
                                    missingDependency
                                )
                            )
                            .ToArray()
                );
            }

            var operationsExecutor = new OperationsExecutor<PackageContext>(installOperations);
            await operationsExecutor.Execute(context);

            CurrentStatus = IOperation<PackageContext>.Status.Complete;

            return context;
        }
    }
}
