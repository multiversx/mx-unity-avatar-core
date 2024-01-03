using UnityEngine;

namespace MultiversX.Avatar.Core.Utilities
{
    public static class StyleHelper
    {
        public static Color ColorFromHex(string hex, double transparency = 1d)
        {
            ColorUtility.TryParseHtmlString(hex, out Color color);
            color.a = (float)transparency;
            return color;
        }
    }
}
