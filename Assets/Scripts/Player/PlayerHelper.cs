using Core;
using UnityEngine;

namespace Player {
    public static class PlayerHelper {
        public static bool HasClickedOnPortal { get; set; }

        public static bool UsingRangedAttack;
        public static Ray GetMouseRay() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }

        public static void ClickedPortal(RaycastHit hit) {
            if (hit.collider == null) return;

            if (Input.GetMouseButtonUp(0) && hit.collider.GetComponent<Portal>() == null)
                HasClickedOnPortal = false;
            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Portal>() != null)
                HasClickedOnPortal = true;
        }
    }
}