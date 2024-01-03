using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiversX.Avatar.Core.Operations
{
    public class Operation<T> : IOperation<T>
        where T : Context
    {
        protected Operation()
        {
            CurrentStatus = IOperation<T>.Status.NotStarted;
        }

        public int Timeout { get; set; }
        public Action<float> ProgressChanged { get; set; }
        public IOperation<T>.Status CurrentStatus { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual async Task<T> Execute(T context, CancellationToken cancellationToken)
        {
            await Task.Delay(1000, cancellationToken);
            throw new NotImplementedException();
        }
    }
}
