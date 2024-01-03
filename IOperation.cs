using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiversX.Avatar.Core
{
    public interface IOperation<T>
        where T : Context
    {
        enum Status
        {
            NotStarted,
            InProgress,
            Complete,
            Failed,
            Cancelled,
        }

        int Timeout { get; set; }
        Action<float> ProgressChanged { get; set; }
        Task<T> Execute(T context, CancellationToken cancellationToken);
        Status CurrentStatus { get; set; }
        string Name { get; set; }
        string Description { get; set; }
    }
}
