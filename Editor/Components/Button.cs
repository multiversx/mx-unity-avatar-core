using System;
using UnityEngine;

namespace MultiversX.Avatar.Core.Editor.Components
{
    public class Button
    {
        private readonly string _label;
        private readonly Action _action;

        public Button(string label, Action action)
        {
            _label = label;
            _action = action;
        }

        public void Draw()
        {
            if (!GUILayout.Button(_label))
                return;

            _action?.Invoke();
        }
    }
}
