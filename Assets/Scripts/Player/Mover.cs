using System;
using Core;
using UnityEngine;
using UnityEngine.AI;
using UnityEngineInternal;

namespace Player {
    public class Mover : MonoBehaviour {
        NavMeshAgent _navMeshAgent;

        void Start() {
            //DontDestroyOnLoad(this.gameObject);
            this._navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            ShouldMovetoMouse();
        }

        public static bool HasClickedOnPortal { get; set; }

        void ShouldMovetoMouse() {
            RaycastHit hit;

            var hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (Input.GetMouseButton(0)) {
                if (hasHit) {
                    Movement(hit.point);
                }
            }
            
            if (Input.GetMouseButtonUp(0) && hit.collider.GetComponent<Portal>() == null) {
                HasClickedOnPortal = false;
            }

            if (Input.GetMouseButtonDown(0) && hit.collider.GetComponent<Portal>() != null) {
                HasClickedOnPortal = true;
            }
            
        }

        void Movement(Vector3 destination) {
            this._navMeshAgent.destination = destination;
        }

        static Ray GetMouseRay() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            return ray;
        }
    }
}