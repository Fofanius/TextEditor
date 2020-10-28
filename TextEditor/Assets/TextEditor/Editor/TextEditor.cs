using System.IO;
using UnityEditor;
using UnityEngine;

namespace TextEditor.Editor
{
    public class TextEditor : EditorWindow
    {
        private string _text;
        private Vector2 _scroll;

        private TextAsset _targetAsset;

        internal static void Open(TextAsset asset)
        {
            var w = CreateInstance<TextEditor>();

            w.TryInitializeText(asset);
            w.Show();
        }

        private void TryInitializeText(TextAsset asset)
        {
            _targetAsset = asset;
            titleContent = new GUIContent(_targetAsset ? _targetAsset.name : "*No asset*");
            RefreshText();
        }

        private void OnEnable()
        {
            minSize = new Vector2(450, 300);
        }

        private void OnGUI()
        {
            if (_targetAsset)
            {
                DrawAsset();
                DrawMenu();
                DrawText();
            }
            else
            {
                DrawEmpty();
            }
        }

        private void DrawAsset()
        {
            GUI.enabled = false;
            {
                EditorGUILayout.ObjectField(_targetAsset, typeof(TextAsset), false);
            }
            GUI.enabled = true;
            EditorGUILayout.LabelField(AssetDatabase.GetAssetPath(_targetAsset), EditorStyles.miniLabel);
        }

        private void DrawEmpty()
        {
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("No asset . . .");
            GUI.color = Color.red;
            {
                if (GUILayout.Button("Close"))
                {
                    Close();
                }
            }
            GUI.color = Color.white;
        }

        private void DrawMenu()
        {
            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Save"))
                {
                    Save();
                }
            }
            EditorGUILayout.EndHorizontal();
        }

        private void Save()
        {
            if (!_targetAsset) return;

            // https://answers.unity.com/questions/12034/change-contents-of-textasset.html
            // способ обновления TextAsset
            File.WriteAllText(AssetDatabase.GetAssetPath(_targetAsset), _text);
            EditorUtility.SetDirty(_targetAsset);

            AssetDatabase.Refresh();
            RefreshText();
        }

        private void DrawText()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                _text = EditorGUILayout.TextArea(_text, GUILayout.ExpandHeight(true));
            }
            EditorGUILayout.EndScrollView();
        }

        private void RefreshText()
        {
            _text = _targetAsset ? _targetAsset.text : string.Empty;
        }
    }
}
