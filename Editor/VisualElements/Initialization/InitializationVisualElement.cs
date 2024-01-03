using System;
using System.Threading.Tasks;
using MultiversX.Avatar.Core.Operations.Managers;
using UnityEngine.UIElements;

namespace MultiversX.Avatar.Core.Editor.VisualElements.Initialization
{
    public class InitializationVisualElement : VisualElement
    {
        private Task _initTask;
        public event Action InitComplete;

        public InitializationVisualElement()
        {
            Label label = new Label(Texts.Initialization);
            OperationsList operationsList = new OperationsList();
            Button initButton = new Button(InitButtonClicked) { text = Texts.Initialize };

            Add(label);
            Add(operationsList);
            Add(initButton);
        }

        private void InitButtonClicked()
        {
            if (_initTask != null)
                return;

            _initTask = InitializationManager.OperationsExecutor.Execute(ProjectContext.Instance);

            _initTask.ContinueWith(_ =>
            {
                _initTask = null;
                InitComplete?.Invoke();
            });
        }
    }
}
