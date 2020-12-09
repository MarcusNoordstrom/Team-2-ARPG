using UnityEngine;

namespace Unit {
    public class LookAtTarget : MonoBehaviour {
        Transform target;
        [SerializeField] Transform partToRotate;
        
        
        void Start() {
            if (partToRotate == null) {
                partToRotate = this.transform;
            }
            enabled = false;
        }

        void FixedUpdate() {
            var lookRotation = Quaternion.LookRotation((target.position - partToRotate.position).normalized, Vector3.up);
            this.partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, 12f * Time.fixedDeltaTime);
        }

        public void Setup(Transform target) {
            this.target = target;
        }
    }
}