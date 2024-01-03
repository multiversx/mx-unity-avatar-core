using UnityEngine.UIElements;

namespace MultiversX.Avatar.Core.Editor.VisualElements.Initialization
{
    public class OperationVisualElement : VisualElement
    {
        public OperationVisualElement(IOperation<ProjectContext> operation)
        {
            style.flexDirection = FlexDirection.Row;
            style.justifyContent = Justify.SpaceBetween;

            Label label = new Label(operation.Name);
            Label status = new Label(operation.CurrentStatus.ToString());

            Add(label);
            Add(status);
        }
    }
}
