﻿using UnityEngine;

namespace Unit {
    public class LookAtTarget : MonoBehaviour {
        Transform target;
        [SerializeField] Transform partToRotate;
        [SerializeField] float yOffset = 3;
        
        void Start() {
            if (partToRotate == null) {
                partToRotate = this.transform;
            }
            enabled = false;
        }

        void FixedUpdate() {
            var lookRotation = Quaternion.LookRotation((new Vector3(target.position.x, target.position.y + yOffset, target.position.z)  - partToRotate.position).normalized, Vector3.up);
            this.partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, 12f * Time.fixedDeltaTime);
        }

        public void Setup(Transform target) {
            this.target = target;
        }
    }
}