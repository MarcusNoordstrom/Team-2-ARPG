using System;
using Player;
using Unit;
using UnityEngine;

namespace UI {
    public class MouseCursor : MonoBehaviour {
        public Texture2D greenCursor, redCursor, blueCursor;

        void Start() {
        }

        void Update() {
            if (PlayerController._ignoreRaycast) return;

            if (Physics.Raycast(PlayerHelper.GetMouseRay(), out var hit)) {
                if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy") || hit.collider.GetComponent<Trap>() != null) {
                    Cursor.SetCursor(redCursor, Vector2.zero, CursorMode.Auto);
                }
                else if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Default")) {
                    Cursor.SetCursor(blueCursor, Vector2.zero, CursorMode.Auto);
                }
                else {
                    Cursor.SetCursor(greenCursor, Vector2.zero, CursorMode.Auto);
                }
            }
        }
    }
}