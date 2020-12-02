using System.Collections;
using UnityEngine;

namespace Unit.Enemy{
    public class Attack : MonoBehaviour{
        public int attackDamage = 10; //Changed to weapon scriptableObject
        public float DPS = 1f;
        private Health targetHealth;

        //TODO fix coroutine!
        private IEnumerable Attacking(){
            float timer = Time.time;
            while (true){
                if ((timer + DPS) >= Time.time){
                    targetHealth.TakeDamage(attackDamage);
                    timer = Time.time;
                }
            }
            yield return null;
        }
            private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
                targetHealth = other.gameObject.GetComponent<Health>();
                StartCoroutine("Attacking");
                //other.GetComponent<Health>().TakeDamage(attackDamage);
            }
        }
        private void OnTriggerExit(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
                StopCoroutine("Attacking");
                targetHealth = null;
            }
        }
    }
}

