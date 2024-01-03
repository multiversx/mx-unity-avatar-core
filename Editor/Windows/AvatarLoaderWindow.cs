using MultiversX.Avatar.Core.Operations.Managers;
using MultiversX.Avatar.Core.Editor.VisualElements.Initialization;
using UnityEditor;
using UnityEngine;

namespace MultiversX.Avatar.Core.Editor.Windows
{
    public class AvatarLoaderWindow : MxEditorWindow
    {
        private InitializationVisualElement _initView;

        public AvatarLoaderWindow()
        {
            InitializationManager.Update += Repaint;
        }

        [MenuItem("MultiversX/Reinitialize Avatar Loader", false, 99)]
        public static void ShowWindow()
        {
            AvatarLoaderWindow window = GetWindowWithRect<AvatarLoaderWindow>(
                new Rect(0, 0, WindowWidth, WindowHeight),
                true,
                Texts.AvatarLoader
            );

            if (window._initView != null)
            {
                return;
            }

            window._initView = new InitializationVisualElement();
            window.InsertMainContent();
        }

        private void OnGUI()
        {
            InitializationManager.Draw();
        }

        protected override void InsertMainContent()
        {
            Main.Add(_initView);
        }
    }
}
