using System;
using UnityEngine;

namespace Player {
    public class RotateBody : MonoBehaviour {
        void Update() {
            Rotation();
        }

        void Rotation() {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out var hit)) {
                var dir = hit.point - this.transform.position;
                var lookRotation = Quaternion.LookRotation(dir);

                Debug.DrawRay(this.transform.position, dir, Color.blue);
                var rotation = lookRotation.eulerAngles;
                this.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            }
        }
    }
}