using System;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class Mover : MonoBehaviour {
        public float speed = 10;

        public GameObject partToRotate;
        Rigidbody _rigidbody;

        Vector3 _direction;
        float _counter = 0;

        void Start() {
            this._rigidbody = GetComponent<Rigidbody>();
        }

        void Update() {
            Movement(KeyCode.W, Vector3.forward);
            Movement(KeyCode.S, Vector3.back);
            Movement(KeyCode.D, Vector3.right);
            Movement(KeyCode.A, Vector3.left);
        }

        void Movement(KeyCode input, Vector3 direction) {
            if (Input.GetKey(input)) {
                this._rigidbody.AddForce(direction * (this.speed * Time.deltaTime), ForceMode.Impulse);
            }
            else {
                this._rigidbody.velocity = Vector3.zero;
            }
        }
    }
}