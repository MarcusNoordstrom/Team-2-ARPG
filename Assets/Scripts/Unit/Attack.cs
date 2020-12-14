using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Unit {
    [Serializable]public class Attack {
        public Weapon weapon;
        [SerializeField] Transform bulletSpawnPoint;
        [HideInInspector]public string animTrigger;
        [SerializeField] Animator animator;

        [HideInInspector]public float _attackTimer;
        [HideInInspector]public bool _canAttack;
        [HideInInspector]public GameObject _target;
        public VisualEffect visualEffect;
        public bool CanAttack => Time.time - _attackTimer > weapon.attackSpeed;
        Health _health;

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