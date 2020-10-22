using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TextEditor.Editor
{
    public class MenuUtility : UnityEditor.Editor
    {
        [MenuItem("Assets/Create/Text/TEXT", false, 128)]
        public static void CreateTextFile()
        {
            CreateTextAssetPopup.ShowPopup(PathUtility.GetPathBySelectionSafe(), "New text", "txt");
        }

        [MenuItem("Assets/Create/Text/README", false, 128)]
        public static void CreateMarkdownFile()
        {
            CreateTextAssetPopup.ShowPopup(PathUtility.GetPathBySelectionSafe(), "README", "md");
        }

        [OnOpenAsset(50)]
        public static bool OnTryAssetClick(int instanceID, int line)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);

            if (!(obj is TextAsset textAsset) || obj.GetType().IsSubclassOf(typeof(TextAsset))) return false;
            
            TextEditor.Open(textAsset);
            return true;
        }
    }
}
