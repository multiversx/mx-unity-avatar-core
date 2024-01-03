using UnityEngine;
using UnityEngine.UIElements;

namespace MultiversX.Avatar.Core.Editor.VisualElements
{
    public class Footer : VisualElement
    {
        public Footer()
        {
            style.flexShrink = 0;

            VisualElement separator = new VisualElement()
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

            Add(separator);
            Add(new Label(Texts.Version) { style = { unityTextAlign = TextAnchor.MiddleCenter, } });
        }
    }
}
