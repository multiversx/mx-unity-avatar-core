using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace MultiversX.Avatar.Core.Operations
{
    public class InstallPackageFromGitOperation<T> : Operation<T>
        where T : Context
    {
        private readonly string _packageGitUrl;
        private readonly string _packageId;

        protected InstallPackageFromGitOperation(string packageGitUrl, string packageId)
        {
            _packageGitUrl = packageGitUrl;
            _packageId = packageId;
        }

        public override async Task<T> Execute(T context, CancellationToken cancellationToken)
        {
            CurrentStatus = IOperation<T>.Status.InProgress;

            if (Validate() && !ProjectContext.ForceInstall)
            {
                CurrentStatus = IOperation<T>.Status.Complete;
                return context;
            }

            AssetDatabase.Refresh();

            await Task.Delay(1000, cancellationToken);

            AddRequest addRequest = Client.Add(_packageGitUrl);

            while (addRequest.IsCompleted == false)
            {
                await Task.Delay(100, cancellationToken);
            }

            return context;
        }

        private bool Validate()
        {
            ListRequest response = Client.List();

            while (!response.IsCompleted)
            {
                Task.Delay(100).Wait();
            }

            return response.Result.Any(packageInfo => packageInfo.name == _packageId);
        }
    }
}
