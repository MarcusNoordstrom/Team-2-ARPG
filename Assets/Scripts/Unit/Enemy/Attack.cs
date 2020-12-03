using System.Collections;
using UnityEngine;

namespace Unit.Enemy{
    public class Attack : MonoBehaviour{
        [SerializeField] private int damage = 10; //Part of weaponSO
        [SerializeField] private float speed = 2f; //Part of weaponSO
        //[SerializeField]public float range = 10;//Part of weaponSO
        private IEnumerator attacking;
        private Health targetHealth;
        private float timer;
        

        private void Awake(){
            this.timer = -this.speed;
        }

        private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer != LayerMask.NameToLayer("Player"))
                return;
            this.targetHealth = other.gameObject.GetComponent<Health>();
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
            while (true){
                yield return new WaitForSeconds(0.5f); //Check what works best between 0.1f and 1f!
                if (!(Time.time - this.timer > this.speed)) continue;
                this.targetHealth.TakeDamage(this.damage); //Temp!
                this.timer = Time.time;
            }
        }
    }
}