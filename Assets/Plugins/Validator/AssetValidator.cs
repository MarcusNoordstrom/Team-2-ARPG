using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Color = UnityEngine.Color;

namespace Validator {
    public class AssetValidator : EditorWindow
    {
        static List<GameObject> _objectsToPing = new List<GameObject>();
        public static Color ErrorColor = Color.red;
        public static List<GameObject> NullReferenceErrorObjects = new List<GameObject>();
        static Texture2D _texture2D = new Texture2D(10, 10);

        static void RefreshHierarchy() {
            DestroyImmediate(GameObject.CreatePrimitive(PrimitiveType.Cube));
        }

        static Color[] BoxColor() {
            var array = _texture2D.GetPixels();
            for (var i = 0; i < array.Length; i++) {
                array[i] = ErrorColor;
            }
            return array;
        }
        
        public static void Validate() {
            _texture2D = new Texture2D(10, 10);
            NullReferenceErrorObjects = new List<GameObject>();
            _objectsToPing = new List<GameObject>();
            var allGameObjects = FindObjectsOfType<GameObject>();

            foreach (var go in allGameObjects) {
                if (!go.activeInHierarchy) continue;
                var components = go.GetComponents<Component>();
                foreach (var component in components) {
                    //TODO: HERE We can compare component like this "if(component is SpriteRenderer)" for example 
                    if (component == null) {
                        _objectsToPing.Add(go);
                    }
                }
            }
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
            NullReferenceErrorObjects = _objectsToPing;
            RefreshHierarchy();
        }
        
        static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect hierarchyRect) {
            var obj = EditorUtility.InstanceIDToObject(instanceID);
            GameObject toRemove = null;
            
            foreach (var t in _objectsToPing.Where(t => obj == t)) {
                var offsetRect = new Rect(hierarchyRect.position + new Vector2(18, 0), hierarchyRect.size);
                var icon = EditorGUIUtility.ObjectContent(EditorUtility.InstanceIDToObject(instanceID), null);
                
                 EditorGUI.DrawRect(hierarchyRect, Color.red);
                
                _texture2D.SetPixels(BoxColor());
                _texture2D.Apply();
                
                //Rect rect = new Rect(hierarchyRect.position + new Vector2(-25, 5), hierarchyRect.size * 0.05f);
                //GUI.DrawTexture(rect, _texture2D);
                
                
                EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle() {
                    normal = new GUIStyleState() { textColor = Color.white },
                    fontStyle = FontStyle.Bold});
                
                GUI.DrawTexture(new Rect(hierarchyRect.xMin, hierarchyRect.yMin, 16, 16), icon.image);
                
                //Checks whenever we actually HAS assigned a script to the object that was null-referenced.
                if (t.gameObject.GetComponent<MonoBehaviour>() != null) {
                    toRemove = t;
                    //Draw Original Stuff
                    EditorGUI.DrawRect(hierarchyRect, new Color(0, 0, 0, 0));
                    EditorGUI.LabelField(offsetRect, obj.name, new GUIStyle() {
                        normal = new GUIStyleState() { textColor = Color.blue },
                        fontStyle = FontStyle.Bold});
                    GUI.DrawTexture(new Rect(hierarchyRect.xMin, hierarchyRect.yMin, 16, 16), icon.image);
                }
            }
            //Removes the object from the list.
            _objectsToPing.Remove(toRemove);
            RefreshHierarchy();
        }
    }
}
