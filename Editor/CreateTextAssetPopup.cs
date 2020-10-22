using UnityEditor;
using UnityEngine;

namespace TextEditor.Editor
{
    public class CreateTextAssetPopup : EditorWindow
    {
        private string _targetPath;
        private string _fileName;
        private string _extension;

        public static void ShowPopup(string path, string fileName, string extension)
        {
            var window = CreateInstance<CreateTextAssetPopup>();

            window.titleContent = new GUIContent($"Create [{extension}]");

            window._extension = extension;
            window._fileName = fileName;
            window._targetPath = path;

            window.position = new Rect(Screen.currentResolution.width / 2 - 175, Screen.currentResolution.height / 2 - 40, 350, 40);
            window.minSize = window.maxSize = new Vector2(350, 80);

            window.ShowModalUtility();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField($"Path: {_targetPath}");
            _fileName = EditorGUILayout.TextField("File name", _fileName);

            GUI.enabled = PathUtility.IsValidFileName(_fileName);
            {
                if (GUILayout.Button($"Create: {(_fileName?.Length > 0 ? _fileName : "-")}.{_extension}", GUILayout.Height(22f)))
                {
                    CreateTextAssetUtility.CreateAssetSafe(_targetPath, _fileName, _extension);
                    Close();
                }
            }
            GUI.enabled = true;
        }
    }
}
