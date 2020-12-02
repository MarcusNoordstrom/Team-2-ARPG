using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

namespace Unit.Enemy{
    public class Attack : MonoBehaviour{
        public int damage = 10; //Part of weaponSO
        public float speed = 5f;//Part of weaponSO
        public float range = 10;//Part of weaponSO
        private Health targetHealth;
        private IEnumerator attacking;
        private float timer;
        
        //TODO: Change TakeDamage to startAttack that can deal dmg ex bullet or range of melee weapon.
        private IEnumerator Attacking(){
            while (true){
                yield return new WaitForSeconds(0.1f);
                if ((Time.time - timer) > speed){
                    targetHealth.TakeDamage(damage);//Change this!
                    timer = Time.time;
                }
            }
        }
            private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
                targetHealth = other.gameObject.GetComponent<Health>();
                if (attacking != null)
                    StopCoroutine(attacking);
                attacking = Attacking();
                StartCoroutine((attacking));
            }
        }
        private void OnTriggerExit(Collider other){
            if (other.gameObject.layer == LayerMask.NameToLayer("Player")){
                StopCoroutine(attacking);
                targetHealth = null;
            }
        }
    }
}

