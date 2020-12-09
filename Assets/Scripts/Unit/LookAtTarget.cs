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
            this.partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, 10f * Time.deltaTime);
        }

        public void Setup(Transform target) {
            this.target = target;
        }
    }
}