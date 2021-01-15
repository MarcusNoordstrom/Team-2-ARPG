using System.IO;
using UnityEditor;
using UnityEngine;

namespace Plugins.MoveFiles {
    public static class MoveFile {
        public static void ShouldMoveFile(string fromFilePath, string nameSpace) {
            var pathToMoveTo = "";
            var fileName = "";
            if ((pathToMoveTo = CheckIfFolderExists(nameSpace)) != "") {
                fileName = Path.GetFileName(fromFilePath);
            }
            else {
                pathToMoveTo = "Assets/Scripts";
                fileName = Path.GetFileName(fromFilePath);
                AssetDatabase.CreateFolder(pathToMoveTo, nameSpace);
                pathToMoveTo += "/" + nameSpace;
            }

            AssetDatabase.MoveAsset(fromFilePath, pathToMoveTo + "/" + fileName);
            Debug.Log($"Moved file {fromFilePath} to {pathToMoveTo}/{fileName} ");
        }

        static string CheckIfFolderExists(string folderToLookFor) {
            var dir = "Assets";

            while (true) {
                if (AssetDatabase.GetSubFolders(dir).Length == 0 || AssetDatabase.GetSubFolders(dir) == null) {
                    return "";
                }

                foreach (var folders in AssetDatabase.GetSubFolders(dir)) {
                    if (folders.EndsWith(folderToLookFor)) {
                        return folders;
                    }

                    dir = folders;
                }
            }
        }
    }
}