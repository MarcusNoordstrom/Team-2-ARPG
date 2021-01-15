using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

namespace Plugins.GetType {
    public static class GetTypeBy {
        public static Dictionary<string, string> NameSpace() {
            var namespacesByFilePath = new Dictionary<string, string>();
            var fileNamesByPath = GetFileNamesByPath("Assets");
            var filePathsByFileName = GroupFilePathsByFileName(fileNamesByPath);

            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies()) {
                if (!assembly.FullName.Contains("Assembly-CSharp"))
                    continue;
                foreach (var type in assembly.GetTypes()) {
                    if (type.Name.Contains("<")) continue;
                    Debug.Log(type.Name);
                    AddNamespacesByFilePathsToDictionaryForType(type, filePathsByFileName, namespacesByFilePath);
                }
            }

            return namespacesByFilePath;
        }

        static void AddNamespacesByFilePathsToDictionaryForType(Type type, Dictionary<string, string[]> filePathsByFileName, Dictionary<string, string> namespacesByFilePath) {
            foreach (var filePath in filePathsByFileName[type.Name]) {
                namespacesByFilePath.Add(filePath, type.Namespace);
            }
        }

        static Dictionary<string, string> GetFileNamesByPath(string path) {
            var result = new Dictionary<string, string>();
            AddFileNamesByPath(path, result);
            return result;
        }

        static void AddFileNamesByPath(string path, Dictionary<string, string> fileNamesByPath) {
            foreach (var filePath in Directory.GetFiles(path)) {
                if (Path.GetExtension(filePath) != ".cs")
                    continue;
                fileNamesByPath.Add(filePath, Path.GetFileNameWithoutExtension(filePath));
            }

            foreach (var directoryPath in Directory.GetDirectories(path)) {
                AddFileNamesByPath(directoryPath, fileNamesByPath);
            }
        }

        // This creates a dictionary, where we can look up file paths by their file name
        // Instead having to search through ALL PATHS to find the right filename
        // for comparison: 
        // performance before with 10.000 Files:
        // For 10.000 Types, look through 10.000 Paths => 10.000 * 10.000 = 100.000.000 Operations
        // now:
        // Group 10.000 Paths, then look at 10.000 Types => 10.000 + 10.000 = 20.000 Operations

        static Dictionary<string, string[]> GroupFilePathsByFileName(Dictionary<string, string> fileNamesByPath) {
            return fileNamesByPath
                .GroupBy(pair => pair.Value, pair => pair.Key)
                .ToDictionary(
                    group => group.Key,
                    group => group.ToArray());
        }
    }
}