using System.IO;
using UnityEditor;
using UnityEngine;

namespace TextEditor.Editor
{
    public static class PathUtility
    {
        /// <summary>
        /// Проверяет, что название не содержит недопустимых символов.
        /// </summary>
        public static bool IsValidFileName(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName)) return false;

            // https://stackoverflow.com/questions/4650462/easiest-way-to-check-if-an-arbitrary-string-is-a-valid-filename
            return fileName.IndexOfAny(Path.GetInvalidFileNameChars()) < 0;
        }

        /// <summary>
        /// Если есть выделение в Project вкладке, то вернет путь относительно него, иначе вернет корневой путь ассетов.
        /// </summary>
        public static string GetPathBySelectionSafe()
        {
            var active = Selection.activeObject;
            if (!active) return Application.dataPath;

            var databasePath = AssetDatabase.GetAssetPath(active);

            if (!(active is DefaultAsset))
            {
                databasePath = Path.GetDirectoryName(databasePath);
            }

            // костыль: если выделена рутовая папка или служебная папка пакетов
            if (databasePath.Equals("Assets") || databasePath.Equals("Packages")) return Application.dataPath;

            return Path.Combine(Application.dataPath, databasePath.Remove(0, 7));
        }
    }
}
