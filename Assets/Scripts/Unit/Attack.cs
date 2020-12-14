using UnityEngine;
using UnityEngine.VFX;

namespace Unit {
    public class Attack : MonoBehaviour {
        public Weapon weapon;
        [SerializeField] Transform bulletSpawnPoint;
        public string animTrigger;
        [SerializeField] Animator animator;

        float _attackTimer;
        bool _canAttack;
        GameObject _target;
        public VisualEffect visualEffect;
        bool CanAttack => Time.time - _attackTimer > weapon.attackSpeed;
        Health _health;
        
        void Update() {
            if (!_canAttack) return;

            if (!CanAttack) return;

            if (animTrigger != null || animator != null) {
                animator.SetTrigger("Shoot");
            }
            weapon.Attack(this.transform, _target);
            
            _attackTimer = Time.time;
        }

        //TODO rename the animation event to "EnemyAttacking"?
        //animation event
        void TurretShooting() {
            visualEffect.Play();
        }

        public void ChangeWeapon(Weapon weapon) {
            this.weapon = weapon;
        }

        //TODO: method that calls attack method based on current weapon.
        public void ActivateAttack(GameObject target) {
            //if(!this.CanAttack) return;
            if (_canAttack) return;
            _target = target;
            _health = target.GetComponent<Health>();
            _canAttack = true;
        }

        public void DeactivateAttack() {
            _canAttack = false;
        }

        //TODO: Fix attack delay on next attack!
    }
}