using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Operations;
using MultiversX.Avatar.Core.Operations.Managers;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace MultiversX.Avatar.Core.Utilities
{
    [InitializeOnLoad]
    public static class PackageInitializer
    {
        private static readonly PackageContext PackageContext = new PackageContext();

        private static readonly Module AvatarsLoaderModule = new Module
        {
            Id = "MultiversX.Avatar.Loader",
            GitUrl = Constants.ModuleGitUrl,
            LocalPath = "Modules/Loader",
        };

        private static readonly Module[] Modules = { AvatarsLoaderModule, };

        static PackageInitializer()
        {
            Initialize();
        }

        private static void Initialize()
        {
            Debug.Log("Initializing Avatar Teleporter package");

            var initializeOperations = new IOperation<PackageContext>[]
            {
                new InitializeOperationCheckMode(),
                new CopyConfigsOperation<PackageContext>(),
                new InitializeOperationInstallDependencies(),
                new InitializeOperationInstallModules()
            };

            var operationsExecutor = new OperationsExecutor<PackageContext>(initializeOperations);

            Debug.Log("Checking for package modules");
            Module[] missingModules = GetMissingPackages(Modules);
            if (missingModules.Length > 0)
            {
                Debug.Log("There are missing modules");

                foreach (Module missingModule in missingModules)
                {
                    Debug.Log($"Installing {missingModule.Id}");
                }
            }

            Task<PackageContext> _ = operationsExecutor.Execute(PackageContext);

            Debug.Log("Avatar Teleporter package initialization complete");
        }

        private static T[] GetMissingPackages<T>(IEnumerable<T> packages)
            where T : Package
        {
            return packages
                .Where(
                    package =>
                        GetInstalledPackages()
                            .All(installedPackage => installedPackage.name != package.Id)
                )
                .ToArray();
        }

        private static IEnumerable<PackageInfo> GetInstalledPackages()
        {
            const int timeOut = 10000;
            int startTimestamp = System.Environment.TickCount;

            ListRequest listRequest = Client.List();

            do
            {
                if (System.Environment.TickCount - startTimestamp > timeOut)
                {
                    throw new System.Exception("Package list request timed out");
                }

                Task.Delay(Constants.StatusCheckIntervalMs).Wait();
            } while (!listRequest.IsCompleted);

            if (listRequest.Status == StatusCode.Success)
            {
                return listRequest.Result.ToArray();
            }

            throw new System.Exception("Package list request failed");
        }
    }
}
