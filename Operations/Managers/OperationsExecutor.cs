using System;
using System.Threading;
using System.Threading.Tasks;

namespace MultiversX.Avatar.Core.Operations.Managers
{
    public class OperationsExecutor<T>
        where T : Context
    {
        private readonly IOperation<T>[] _operations;
        private CancellationTokenSource _cancellationTokenSource;
        private readonly int _operationsCount;
        private int _currentOperationIndex;
        private float _progress;

        public int Timeout { get; set; }
        public event Action AllCompleted;
        public Action<IOperation<T>> OperationComplete;
        public Action<IOperation<T>> OperationFailed;
        public Action<IOperation<T>> OperationCancelled;
        private bool _inProgress;
        public event Action<float, string> ProgressChanged;

        public OperationsExecutor(IOperation<T>[] operations)
        {
            _operations = operations;
            _operationsCount = operations.Length;
        }

        public void Cancel()
        {
            _cancellationTokenSource.Cancel();
            OperationCancelled?.Invoke(_operations[_currentOperationIndex]);
        }

        public async Task<T> Execute(T context)
        {
            _inProgress = true;
            _cancellationTokenSource = new CancellationTokenSource();

            foreach (IOperation<T> operation in _operations)
            {
                ProgressChanged?.Invoke(
                    _currentOperationIndex / (float)_operationsCount,
                    operation.Description
                );

                operation.ProgressChanged += OnProgressChanged;
                operation.Timeout = Timeout;
                operation.CurrentStatus = IOperation<T>.Status.InProgress;

                try
                {
                    context = await operation.Execute(context, _cancellationTokenSource.Token);
                    operation.CurrentStatus = IOperation<T>.Status.Complete;
                }
                catch
                {
                    if (_cancellationTokenSource.IsCancellationRequested)
                    {
                        _cancellationTokenSource.Dispose();
                        operation.CurrentStatus = IOperation<T>.Status.Cancelled;
                    }
                    else
                    {
                        operation.CurrentStatus = IOperation<T>.Status.Failed;
                        OperationFailed?.Invoke(operation);
                    }

                    _inProgress = false;
                    throw;
                }

                operation.ProgressChanged -= OnProgressChanged;

                OperationComplete?.Invoke(operation);

                if (_currentOperationIndex < _operations.Length - 1)
                {
                    _currentOperationIndex++;
                }
                else
                {
                    ProjectContext.ForceInstall = false;
                }
            }

            _inProgress = false;
            AllCompleted?.Invoke();
            return context;
        }

        private void OnProgressChanged(float progress)
        {
            _progress = 1f / _operationsCount * (_currentOperationIndex + progress);
            ProgressChanged?.Invoke(_progress, _operations[_currentOperationIndex].GetType().Name);
        }

        public bool IsInProgress()
        {
            return _inProgress;
        }

        public void ResetOperations()
        {
            Cancel();

            _currentOperationIndex = 0;
            _progress = 0;

            foreach (IOperation<T> operation in _operations)
            {
                operation.CurrentStatus = IOperation<T>.Status.NotStarted;
            }
        }
    }
}
