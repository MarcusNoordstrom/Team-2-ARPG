using System.Collections;
using UnityEngine;

namespace Unit.Enemy{
    public class Attack : MonoBehaviour{
        [SerializeField]private int damage = 10; //Part of weaponSO
        [SerializeField]public float speed = 5f;//Part of weaponSO
        //[SerializeField]public float range = 10;//Part of weaponSO
        private Health _targetHealth;
        private IEnumerator _attacking;
        private float _timer;
        private void Awake(){
            _timer = -speed;
        }
        //TODO: Change TakeDamage to startAttack that can deal dmg ex bullet or range of melee weapon.
        private IEnumerator Attacking(){
            while (true){
                yield return new WaitForSeconds(0.5f); //Check what works best between 0.1f and 1f!
                if (!(Time.time - _timer > speed)) continue;
                _targetHealth.TakeDamage(damage); //Temp!
                _timer = Time.time;
            }
        }
        private void OnTriggerEnter(Collider other){
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) 
                return;
            _targetHealth = other.gameObject.GetComponent<Health>();
            if (_attacking != null)
                StopCoroutine(_attacking);
            _attacking = Attacking();
            StartCoroutine((_attacking));
        }
        private void OnTriggerExit(Collider other){
            if (other.gameObject.layer != LayerMask.NameToLayer("Player")) 
                return;
            StopCoroutine(_attacking);
            _targetHealth = null;
        }
    }
}

