using UnityEngine.UIElements;

namespace MultiversX.Avatar.Core.Editor.VisualElements.Initialization
{
    public class OperationsList : VisualElement
    {
        public OperationsList()
        {
            foreach (IOperation<ProjectContext> operation in ProjectContext.ConfigureOperations)
            {
                Add(new OperationVisualElement(operation));
            }
        }
    }
}
