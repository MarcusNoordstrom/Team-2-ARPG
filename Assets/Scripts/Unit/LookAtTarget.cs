using UnityEngine;

namespace Unit{


    public class LookAtTarget : MonoBehaviour{
        private Transform target;

        private void Awake(){
            this.enabled = false;
        }

        public void Setup(Transform target){
            this.target = target;
        }

        private void FixedUpdate(){
            Quaternion lookRotation = Quaternion.LookRotation((this.target.position - transform.position).normalized);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10f * Time.deltaTime);
        }
    }
}
