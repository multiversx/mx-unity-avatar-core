using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;
using PackageInfo = UnityEditor.PackageManager.PackageInfo;

namespace MultiversX.Avatar.Core.Utilities
{
    public static class PackageHelpers
    {
        public static Task<PackageInfo[]> GetInstalledPackagesAsync()
        {
            var tcs = new TaskCompletionSource<PackageInfo[]>();
            ListRequest listRequest = Client.List();
            EditorApplication.update += OnUpdate;
            return tcs.Task;

            void OnUpdate()
            {
                if (!listRequest.IsCompleted)
                    return;

                if (listRequest.Status == StatusCode.Success)
                {
                    tcs.SetResult(listRequest.Result.ToArray());
                }
                else
                {
                    tcs.SetException(new System.Exception("Failed to get installed packages"));
                }

                EditorApplication.update -= OnUpdate;
            }
        }

        public static async Task<T[]> GetMissingPackagesAsync<T>(IEnumerable<T> requiredPackages)
            where T : Package
        {
            PackageInfo[] installedPackages = await GetInstalledPackagesAsync();
            return requiredPackages
                .Where(
                    requiredPackage =>
                        installedPackages.All(
                            installedPackage => installedPackage.name != requiredPackage.Id
                        )
                )
                .ToArray();
        }

        public static Task<TPackage> InstallPackageAsync<TPackage>(TPackage package)
            where TPackage : Package
        {
            var tcs = new TaskCompletionSource<TPackage>();
            AddRequest addRequest = Client.Add(
                typeof(TPackage) == typeof(Module) && PackageContext.LocalMode
                    ? (package as Module)?.LocalPath
                    : package.GitUrl
            );
            EditorApplication.update += OnUpdate;
            return tcs.Task;

            void OnUpdate()
            {
                if (!addRequest.IsCompleted)
                    return;

                if (addRequest.Status == StatusCode.Success)
                {
                    tcs.SetResult(package);
                }
                else
                {
                    tcs.SetException(new System.Exception($"Failed to install {package.Id}"));
                }

                EditorApplication.update -= OnUpdate;
            }
        }
    }
}
