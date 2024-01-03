using MultiversX.Avatar.Core.Editor.VisualElements;
using UnityEditor;
using UnityEngine.UIElements;

namespace MultiversX.Avatar.Core.Editor.Windows
{
    public abstract class MxEditorWindow : EditorWindow
    {
        protected const float WindowWidth = 300;
        protected const float WindowHeight = 500;
        protected VisualElement Main;

        public void CreateGUI()
        {
            rootVisualElement.style.width = WindowWidth;
            rootVisualElement.style.height = WindowHeight;
            rootVisualElement.style.backgroundColor = Constants.BackgroundColor;
            rootVisualElement.style.display = DisplayStyle.Flex;
            rootVisualElement.style.flexDirection = FlexDirection.Column;
            rootVisualElement.style.justifyContent = Justify.SpaceBetween;
            rootVisualElement.style.height = WindowHeight;
            rootVisualElement.style.paddingTop = 16;
            rootVisualElement.style.paddingBottom = 16;
            rootVisualElement.style.paddingLeft = 16;
            rootVisualElement.style.paddingRight = 16;

            Header header = new Header();
            Main = new VisualElement
            {
                style = { flexGrow = 1, justifyContent = Justify.SpaceBetween }
            };
            Footer footer = new Footer();

            rootVisualElement.Add(header);
            rootVisualElement.Add(Main);
            rootVisualElement.Add(footer);

            InsertMainContent();
        }

        protected abstract void InsertMainContent();
    }
}
