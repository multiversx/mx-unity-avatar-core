using System.Threading;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Utilities;

namespace MultiversX.Avatar.Core.Operations
{
    public class CopyConfigsOperation<T> : Operation<T>
        where T : Context
    {
        public CopyConfigsOperation()
        {
            Name = Texts.CopyConfigurationFiles;
            Description = Texts.CopyingConfigurationFiles;
        }

#pragma warning disable CS1998
        public async override Task<T> Execute(T context, CancellationToken cancellationToken)
        {
            CurrentStatus = IOperation<T>.Status.InProgress;

            if (Validate() && !ProjectContext.ForceInstall)
            {
                CurrentStatus = IOperation<T>.Status.Complete;
                return context;
            }

            Config.CopyConfigs();

            return context;
        }
#pragma warning restore CS1998

        public static bool Validate()
        {
            return Config.Validate();
        }
    }
}
