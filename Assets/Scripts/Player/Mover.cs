using Core;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class Mover : MonoBehaviour {
        NavMeshAgent _navMeshAgent;

        void Start() {
            this._navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            ShouldMovetoMouse();
        }

        public static bool HasClickedOnPortal { get; set; }

        void ShouldMovetoMouse() {
            var hasHit = Physics.Raycast(GetMouseRay(), out var hit);
            if (Input.GetMouseButton(0)) {
                if (hasHit) {
                    Movement(hit.point);
                }
            }

            ClickedPortal(hit);
        }
        void Movement(Vector3 destination) {
            this._navMeshAgent.destination = destination;
        }

        static Ray GetMouseRay() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }

        static void ClickedPortal(RaycastHit hit) {
            if (Input.GetMouseButtonUp(0) && hit.collider.GetComponent<Portal>() == null)
                HasClickedOnPortal = false;
            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Portal>() != null)
                HasClickedOnPortal = true;
        }
    }
}