using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class Mover : MonoBehaviour {
        NavMeshAgent _navMeshAgent;

        void Start() {
            //DontDestroyOnLoad(this.gameObject);
            this._navMeshAgent = GetComponent<NavMeshAgent>();
        }

        void Update() {
            if (Input.GetMouseButton(0)) {
                ShouldMovetoMouse();
            }
        }

        void ShouldMovetoMouse() {
            RaycastHit hit;

            var hasHit = Physics.Raycast(GetMouseRay(), out hit);

            if (hasHit) {
                Movement(hit.point);
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