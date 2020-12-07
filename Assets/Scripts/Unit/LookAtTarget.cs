using UnityEngine;

namespace Unit {
    public class LookAtTarget : MonoBehaviour {
        Transform target;

        void Awake() {
            enabled = false;
        }

        void FixedUpdate() {
            var lookRotation = Quaternion.LookRotation((target.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
        }

        public void Setup(Transform target) {
            this.target = target;
        }
    }
}