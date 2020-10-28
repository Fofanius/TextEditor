using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace TextEditor.Editor
{
    public class MenuUtility : UnityEditor.Editor
    {
        [MenuItem("Assets/Create/Text Asset/Plain text", false, 128)]
        public static void CreateTextFile()
        {
            CreateTextAssetPopup.ShowPopup(PathUtility.GetPathBySelectionSafe(), "New text", "txt");
        }

        [MenuItem("Assets/Create/Text Asset/Markdown", false, 128)]
        public static void CreateMarkdownFile()
        {
            CreateTextAssetPopup.ShowPopup(PathUtility.GetPathBySelectionSafe(), "README", "md");
        }
        
        [MenuItem("Assets/Create/Text Asset/Json", false, 128)]
        public static void CreateJsonFile()
        {
            CreateTextAssetPopup.ShowPopup(PathUtility.GetPathBySelectionSafe(), "New json", "json");
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
