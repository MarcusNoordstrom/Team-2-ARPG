using UnityEngine;

namespace Unit {
    public class LookAtTarget : MonoBehaviour {
        Transform target;
        [SerializeField] Transform partToRotate;
        
        
        void Awake() {
            enabled = false;
            if (partToRotate == null) {
                partToRotate = this.transform;
            }
        }

        void FixedUpdate() {
            var lookRotation = Quaternion.LookRotation((target.position - partToRotate.position).normalized);
            partToRotate.rotation = Quaternion.Slerp(partToRotate.rotation, lookRotation, 10f * Time.deltaTime);
        }

        public void Setup(Transform target) {
            this.target = target;
        }
    }
}