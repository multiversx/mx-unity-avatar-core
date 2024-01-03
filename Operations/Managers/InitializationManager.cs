using System;
using System.Linq;
using MultiversX.Avatar.Core.Editor.Components;
using UnityEditor;
using UnityEngine;

namespace MultiversX.Avatar.Core.Operations.Managers
{
    public static class InitializationManager
    {
        public static readonly OperationsExecutor<ProjectContext> OperationsExecutor;
        private static ProjectContext _projectContext;
        private static readonly Button InitializeButton;
        private static float _progress;
        private static string _progressMessage;

        static InitializationManager()
        {
            InitializeButton = new Button(Texts.Initialize, InitializeButtonHandler);
            OperationsExecutor = new OperationsExecutor<ProjectContext>(
                ProjectContext.ConfigureOperations
            )
            {
                Timeout = 30 * 1000
            };
            OperationsExecutor.OperationComplete += OnOperationComplete;
            OperationsExecutor.OperationFailed += OnOperationFailed;
            OperationsExecutor.ProgressChanged += OnProgressChanged;
        }

        private static void ResetOperations()
        {
            OperationsExecutor.ResetOperations();
        }

        public static event Action Update;

        private static void OnOperationComplete(IOperation<ProjectContext> operation)
        {
            OnUpdate();
        }

        private static void OnOperationFailed(IOperation<ProjectContext> operation)
        {
            OnUpdate();
        }

        private static void OnProgressChanged(float progress, string message)
        {
            _progress = progress;
            _progressMessage = message;
            OnUpdate();
        }

        public static void Draw()
        {
            foreach (IOperation<ProjectContext> operation in ProjectContext.ConfigureOperations)
            {
                Color statusColor;

                switch (operation.CurrentStatus)
                {
                    case IOperation<ProjectContext>.Status.NotStarted:
                        statusColor = Color.white;
                        break;
                    case IOperation<ProjectContext>.Status.InProgress:
                        statusColor = Color.white;
                        break;
                    case IOperation<ProjectContext>.Status.Complete:
                        statusColor = Constants.AccentColor;
                        break;
                    case IOperation<ProjectContext>.Status.Failed:
                        statusColor = Color.red;
                        break;
                    case IOperation<ProjectContext>.Status.Cancelled:
                        statusColor = Color.gray;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                GUILayout.BeginHorizontal();
                GUILayout.Label(operation.Name);
                GUILayout.FlexibleSpace();
                Color color = GUI.color;
                GUI.color = statusColor;
                GUILayout.Label(operation.CurrentStatus.ToString());
                GUI.color = color;
                GUILayout.EndHorizontal();
            }

            if (
                ProjectContext.ConfigureOperations.All(
                    operation =>
                        operation.CurrentStatus == IOperation<ProjectContext>.Status.NotStarted
                )
            )
            {
                InitializeButton.Draw();
            }
            else if (
                ProjectContext.ConfigureOperations.Any(
                    operation =>
                        operation.CurrentStatus == IOperation<ProjectContext>.Status.InProgress
                )
            )
            {
                if (GUILayout.Button(Texts.Cancel))
                {
                    OperationsExecutor.Cancel();
                }

                Rect rect = GUILayoutUtility.GetRect(100, 20);
                EditorGUI.ProgressBar(rect, _progress, _progressMessage);
            }
            else
            {
                if (GUILayout.Button(Texts.ReInitialize))
                {
                    ProjectContext.ForceInstall = true;
                    ResetOperations();
                    Initialize();
                }
            }
        }

        private static async void Initialize()
        {
            ProjectContext.Instance = await OperationsExecutor.Execute(ProjectContext.Instance);
        }

        private static void OnUpdate()
        {
            Update?.Invoke();
        }

        private static void InitializeButtonHandler()
        {
            Initialize();
        }
    }
}
