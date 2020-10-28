using UnityEditor;
using UnityEngine;

namespace TextEditor.Editor
{
    public class CreateTextAssetPopup : EditorWindow
    {
        private const float POPUP_WIDTH = 350;
        private const float POPUP_HEIGHT = 80;

        private string _targetPath;
        private string _fileName;
        private string _extension;

        public static void ShowPopup(string path, string fileName, string extension)
        {
            var window = CreateInstance<CreateTextAssetPopup>();

            window.titleContent = new GUIContent($"Create . . .");

            window._extension = extension;
            window._fileName = fileName;
            window._targetPath = path;

            window.position = new Rect(Screen.currentResolution.width / 2f - POPUP_WIDTH / 2f, Screen.currentResolution.height / 2f - POPUP_HEIGHT / 2, POPUP_WIDTH, POPUP_HEIGHT);
            window.minSize = window.maxSize = new Vector2(POPUP_WIDTH, POPUP_HEIGHT);

            window.ShowModalUtility();
        }

        private void OnGUI()
        {
            EditorGUILayout.Space();

            EditorGUILayout.LabelField($"Path: {_targetPath}");
            EditorGUILayout.BeginHorizontal(EditorStyles.helpBox);
            {
                _fileName = EditorGUILayout.TextField("File name", _fileName);
                _extension = EditorGUILayout.TextField(_extension, GUILayout.Width(25f));
            }
            EditorGUILayout.EndHorizontal();

            GUI.enabled = PathUtility.IsValidFileName(_fileName) && PathUtility.IsValidFileName(_extension);
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
