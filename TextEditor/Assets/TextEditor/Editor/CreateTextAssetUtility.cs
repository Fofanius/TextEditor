using System.IO;
using UnityEditor;

namespace TextEditor.Editor
{
    public static class CreateTextAssetUtility
    {
        /// <summary>
        /// Безопасное создание файла.
        /// <para/>
        /// Если файл уже существует, создатст такой же, но с индексом.
        /// </summary>
        public static void CreateAssetSafe(string directory, string fileName, string extension)
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
