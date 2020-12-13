using UnityEditor;
using UnityEngine;

namespace Editor {
    
    public class ReplaceObject : EditorWindow {
        private GameObject gameObjectToReplace;
        private int currentPickerWindow;
        [MenuItem("Tools/Replace Object")]
        
        

        public static void ShowWindow()
        {
            GetWindow(typeof(ReplaceObject)).ShowUtility();
        }

        void OnSelectionChange()
        {
            gameObjectToReplace = Selection.activeGameObject;
            GetWindow(typeof(ReplaceObject)).ShowUtility();
            Repaint();
        }
        
        void ShowPicker() {
            currentPickerWindow = GUIUtility.GetControlID(FocusType.Passive) + 100;
            EditorGUIUtility.ShowObjectPicker<GameObject>(null,true,string.Empty,currentPickerWindow);
        }

        private void OnGUI()
        {
            var textToDisplay = "Nothing selected.... Select something in the scene."; 
            GUILayout.Label ("Object to replace", EditorStyles.boldLabel);
            
            if (gameObjectToReplace != null) {
                textToDisplay = $"{gameObjectToReplace.name}";
            }
            else {
                textToDisplay = "Nothing selected.... Select something in the scene.";
            }
            
            GUILayout.TextField(textToDisplay);
            
            var button = GUILayout.Button("Replace!");

            if (button) {
                ShowPicker();
            }

            if (Event.current.commandName != "ObjectSelectorUpdated" ||
                EditorGUIUtility.GetObjectPickerControlID() != currentPickerWindow) return;
            var objectToReplaceTo = EditorGUIUtility.GetObjectPickerObject();
            currentPickerWindow = -1;

            GameObject transform = gameObjectToReplace;

            GameObject obj = Instantiate((GameObject)objectToReplaceTo);
            
            obj.transform.position = transform.transform.position;
            obj.transform.rotation = transform.transform.rotation;
            
            DestroyImmediate(transform);
        }
    }
    
    #region Old, keep for safekeepimg
    // public static class ObjectSelector
    // {
    //     private static System.Type T;
    //     private static bool oldState = false;
    //     static ObjectSelectorWrapper()
    //     {
    //         T = System.Type.GetType("UnityEditor.ObjectSelector,UnityEditor");
    //     }
    //  
    //     private static EditorWindow Get()
    //     {
    //         PropertyInfo P = T.GetProperty("get", BindingFlags.Public | BindingFlags.Static);
    //         return P.GetValue(null,null) as EditorWindow;
    //     }
    //     public static void ShowSelector(System.Type aRequiredType)
    //     {
    //         MethodInfo ShowMethod = T.GetMethod("Show",BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
    //         ShowMethod.Invoke(Get (), new object[]{null,aRequiredType,null, true});
    //     }
    //     public static T GetSelectedObject<T>() where T : UnityEngine.Object
    //     {
    //         MethodInfo GetCurrentObjectMethod =  ObjectSelectorWrapper.T.GetMethod("GetCurrentObject",BindingFlags.Static | BindingFlags.Public);
    //         return GetCurrentObjectMethod.Invoke(null,null) as T;
    //     }
    //     public static bool isVisible
    //     {
    //         get 
    //         {
    //             PropertyInfo P = T.GetProperty("isVisible", BindingFlags.Public | BindingFlags.Static);
    //             return (bool)P.GetValue(null,null);
    //         }
    //     }
    //     public static bool HasJustBeenClosed()
    //     {
    //         bool visible = isVisible;
    //         if (visible != oldState && visible == false)
    //         {
    //             oldState = false;
    //             return true;
    //         }
    //         oldState = visible;
    //         return false;
    //     }
    // }
    #endregion
}
