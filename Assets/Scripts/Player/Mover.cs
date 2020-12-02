using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class Mover : MonoBehaviour {
        public float speed = 500;

        Rigidbody _rigidbody;

        void Start() {
            this._rigidbody = GetComponent<Rigidbody>();
        }

        void Update() {
            Movement(KeyCode.W, Vector3.forward);
            Movement(KeyCode.S, Vector3.back);
            Movement(KeyCode.D, Vector3.right);
            Movement(KeyCode.A, Vector3.left);
            RotateTowardsMouse();
        }

        void Movement(KeyCode input, Vector3 direction) {
            if (Input.GetKey(input)) {
                this._rigidbody.AddForce(direction * (this.speed * Time.deltaTime), ForceMode.Impulse);
            }
            else {
                this._rigidbody.velocity = Vector3.zero;
            }
        }

        void RotateTowardsMouse() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (!Physics.Raycast(ray, out var hit, Mathf.Infinity, ~(1 << LayerMask.NameToLayer("Player")))) {
                return;
            }

            var targetPoint = hit.point;
            targetPoint.y = this.transform.position.y;
            this.transform.LookAt(targetPoint, Vector3.up);
            
        }
    }
}