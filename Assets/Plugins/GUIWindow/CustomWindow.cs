using System.Collections.Generic;
using Plugins.GetType;
using Plugins.MoveFiles;
using UnityEngine;
using UnityEditor;
using Validator;

public partial class CustomWindow : EditorWindow {
    private float space = 15;
    private Vector2 scrollPos;
    private bool canPress = true;
    private int SmallBtnWidth = 80;
    private const string files = "files";
    private const string incorrect = "incorrect";
    private const string gameObjects = "gameObjects";
    private List<MonoScript> paths = new List<MonoScript>();
    private List<string> executionOrder = new List<string>();
    private Dictionary<MonoScript, string[]> pathsForIncorrectFolder = new Dictionary<MonoScript, string[]>();

    private bool clearFields;
    int filePath = 0;
    int nameSpaceOfFile = 1;

    private bool ClearFields {
        set {
            if (!(clearFields = value)) return;
            AssetValidator.NullReferenceErrorObjects.Clear();
            pathsForIncorrectFolder.Clear();
            paths.Clear();
            canPress = true;
        }
    }

    [MenuItem("ValidatorTool/Validate")]
    public static void CustomEditorWindow() {
        GetWindow<CustomWindow>("Validator");
    }

    private void OnGUI() {
        GUILayout.Space(space);
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();

        GUILayout.Label("Choose what part of your project to validate");

        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();

        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Validate namespaces")) {
            executionOrder.Remove(files);
            executionOrder.Insert(0, files);
            NameSpacePathArray();
        }

        if (GUILayout.Button("Validate assets in scene")) {
            executionOrder.Remove(gameObjects);
            executionOrder.Insert(0, gameObjects);
            if (!canPress) return;
            canPress = false;
            AssetValidator.Validate();
        }

        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();

        if (GUILayout.Button("Validate namespace in wrong folder")) {
            executionOrder.Remove(incorrect);
            executionOrder.Insert(0, incorrect);
            IsNameSpaceInCorrectFolder();
        }

        if (GUILayout.Button("Clear")) ClearFields = true;

        GUILayout.EndHorizontal();
        scrollPos = EditorGUILayout.BeginScrollView(scrollPos);
        EditorGUILayout.Space(space);


        foreach (var order in executionOrder) {
            if (order == gameObjects)
                foreach (var go in AssetValidator.NullReferenceErrorObjects)
                    EditorGUILayout.ObjectField("Missing Component", go, go.GetType(), false);

            if (order == files)
                foreach (var file in paths)
                    EditorGUILayout.ObjectField("Missing NameSpace", file, file.GetType(), false);

            if (order == incorrect)
                foreach (var file in pathsForIncorrectFolder) {
                    GUILayout.BeginHorizontal();

                    EditorGUILayout.ObjectField("Wrong folder", file.Key, file.Key.GetType(), false);
                    if (GUILayout.Button("Move", GUILayout.Width(this.SmallBtnWidth)))
                        MoveFile.ShouldMoveFile(file.Value[this.filePath], file.Value[this.nameSpaceOfFile]);

                    GUILayout.EndHorizontal();
                }
        }

        EditorGUILayout.EndScrollView();
    }

    private void NameSpacePathArray() {
        paths.Clear();
        foreach (var d in GetTypeBy.NameSpace())
            if (string.IsNullOrEmpty(d.Value)) {
                var file = (MonoScript) AssetDatabase.LoadAssetAtPath(d.Key, typeof(TextAsset));
                paths.Add(file);
            }
    }

    private void IsNameSpaceInCorrectFolder() {
        pathsForIncorrectFolder.Clear();
        foreach (var d in GetTypeBy.NameSpace()) {
            if (string.IsNullOrEmpty(d.Value)) continue;
            var newStr = d.Value;
            if (d.Value.Contains(".")) {
                var str = d.Value.Split('.');
                newStr = str[str.Length - 1];
            }

            if (!d.Key.Contains(newStr)) {
                var file = (MonoScript) AssetDatabase.LoadAssetAtPath(d.Key, typeof(TextAsset));
                pathsForIncorrectFolder.Add(file, new[] {d.Key, d.Value});
            }
        }
    }
}