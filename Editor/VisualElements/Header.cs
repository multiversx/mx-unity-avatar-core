using UnityEngine;
using UnityEngine.UIElements;

namespace MultiversX.Avatar.Core.Editor.VisualElements
{
    public class Header : VisualElement
    {
        public Header()
        {
            var label = new Label(Texts.MultiversxAvatarLoader)
            {
                style =
                {
                    unityTextAlign = TextAnchor.MiddleCenter,
                    fontSize = 16,
                    color = Constants.AccentColor
                }
            };

            var separator = new VisualElement()
            {
                style =
                {
                    backgroundColor = Constants.AccentColor,
                    height = 1,
                    width = 100,
                    marginTop = 16,
                    marginBottom = 16,
                    alignSelf = Align.Center,
                    flexShrink = 0
                }
            };

            Add(label);
            Add(separator);
        }
    }
}
