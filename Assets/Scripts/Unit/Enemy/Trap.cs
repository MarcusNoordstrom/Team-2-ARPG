using System.Collections;
using UnityEngine;

namespace Unit {
    public class Trap : MonoBehaviour{
        [SerializeField] private int damage = 10;
        [SerializeField] private float speed = 2f;
        private IEnumerator attacking;
        private Health targetHealth;
        private float timer;

        private void Awake(){
            timer = -speed;
        }

        private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            targetHealth = other.gameObject.GetComponent<Health>();
            if (this.attacking != null)
                StopCoroutine(this.attacking);
            this.attacking = Attacking();
            StartCoroutine(this.attacking);
        }

        private void OnTriggerExit(Collider other){
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            StopCoroutine(this.attacking);
            this.targetHealth = null;
        }
        
        //TODO: Change TakeDamage to startAttack that can deal dmg ex bullet or range of melee weapon.
        private IEnumerator Attacking(){
            while (targetHealth.CurrentHealth > 0){
                yield return new WaitForSeconds(0.5f); //Check what works best between 0.1f and 1f!
                if (!(Time.time - this.timer > this.speed)) continue;
                this.targetHealth.TakeDamage(this.damage); //Temp!
                this.timer = Time.time;
            }
            yield break;
        }
    }
}