using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Operations.Managers;
using MultiversX.Avatar.Core.Utilities;
using UnityEngine;

namespace MultiversX.Avatar.Core.Operations
{
    public class InitializeOperationInstallModules : Operation<PackageContext>
    {
        private static readonly Module[] Modules =
        {
            new Module
            {
                Id = "MultiversX.Avatar.Loader",
                GitUrl = Constants.ModuleGitUrl,
                LocalPath = "unity-avatar-loader",
            },
        };

        public override async Task<PackageContext> Execute(
            PackageContext context,
            CancellationToken cancellationToken
        )
        {
            if (PackageContext.LocalMode)
            {
                Debug.Log("Local mode detected, skipping module installation. Please install modules manually as required.");
                return context;
            }
            
            IOperation<PackageContext>[] installOperations = { };

            CurrentStatus = IOperation<PackageContext>.Status.InProgress;

            Module[] missingDependencies = await PackageHelpers.GetMissingPackagesAsync(Modules);

            if (missingDependencies.Length > 0)
            {
                installOperations = missingDependencies.Aggregate(
                    installOperations,
                    (current, missingDependency) =>
                        current
                            .Append(
                                new PackageInstallOperation<PackageContext, Module>(
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
