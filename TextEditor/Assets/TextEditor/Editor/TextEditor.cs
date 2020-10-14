﻿using System.IO;
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

            w.titleContent = new GUIContent(asset.name);
            w.TryInitializeText(asset);

            w.Show();
        }

        private void TryInitializeText(TextAsset asset)
        {
            _targetAsset = asset;
            if (_targetAsset)
            {
                _text = _targetAsset.text;
            }
        }

        private void OnGUI()
        {
            DrawMenu();
            DrawText();
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
            if (_targetAsset)
            {
                // https://answers.unity.com/questions/12034/change-contents-of-textasset.html
                // способ обновления TextAsset
                File.WriteAllText(AssetDatabase.GetAssetPath(_targetAsset), _text);
                EditorUtility.SetDirty(_targetAsset);
            }
            
            AssetDatabase.Refresh();
        }

        private void DrawText()
        {
            _scroll = EditorGUILayout.BeginScrollView(_scroll);
            {
                _text = EditorGUILayout.TextArea(_text);
            }
            EditorGUILayout.EndScrollView();
        }
    }
}