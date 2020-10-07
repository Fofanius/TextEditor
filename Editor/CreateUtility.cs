using System.IO;
using UnityEditor;
using UnityEngine;

namespace TextEditor.Editor
{
    public class CreateUtility : UnityEditor.Editor
    {
        [MenuItem("Assets/Create/Text/Empty text file", false, 0)]
        public static void CreateTextFile()
        {
            CreateAssetSafe(Application.dataPath, "README", "txt");
        }

        /// <summary>
        /// Безопасное создание файла.
        /// <para/>
        /// Если файл уже существует, создатст такой же, но с индексом.
        /// </summary>
        private static void CreateAssetSafe(string directory, string fileName, string extension)
        {
            var path = $"{directory}/{fileName}.{extension}";

            if (File.Exists(path))
            {
                var subIndex = 1;

                while (true)
                {
                    var subPath = $"{directory}/{fileName} {subIndex++}.{extension}";
                    if (File.Exists(subPath)) continue;

                    File.Create(subPath).Dispose();
                    break;
                }
            }
            else
            {
                File.Create(path).Dispose();
            }

            AssetDatabase.Refresh();
        }
    }
}