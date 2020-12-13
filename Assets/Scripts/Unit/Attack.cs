﻿using UnityEngine;

namespace Unit {
    public class Attack : MonoBehaviour {
        public Weapon weapon;
        [SerializeField] Transform bulletSpawnPoint;
        public string animTrigger;
        [SerializeField] Animator animator;

        float _attackTimer;
        Bullet _bulletPrefab;
        bool _canAttack;
        Health _health;
        GameObject _target;
        bool CanAttack => Time.time - _attackTimer > weapon.attackSpeed;

        void Update() {
            if (!_canAttack) return;

            if (!CanAttack) return;

            if (animTrigger != null || animator != null) {
                TurretShoot();
            }

            if (weapon is RangeWeapon)
                SpawnBullet();
            else
                Melee();
            _attackTimer = Time.time;
        }

        void TurretShoot() {
            animator.SetTrigger("Shoot");
        }

        public void ChangeWeapon(Weapon weapon) {
            this.weapon = weapon;
            if (this.weapon is RangeWeapon rangeWeapon) _bulletPrefab = rangeWeapon.bulletPrefab;
        }

        //TODO: method that calls attack method based on current weapon.
        public void ActivateAttack(GameObject target) {
            //if(!this.CanAttack) return;
            if (_canAttack) return;
            _target = target;
            _health = target.GetComponent<Health>();
            _canAttack = true;
        }

        public void SpawnBullet() {
            var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.BulletFiredBy = LayerMask.GetMask();
            bullet.Setup(_target, weapon.baseDamage);
        }

        public void DeactivateAttack() {
            _canAttack = false;
        }

        //TODO: Either use raycast, enable a gameobject or spherecast.
        void Melee() {
            _health.TakeDamage(weapon.baseDamage);
        }

        //TODO: Fix attack delay on next attack!
    }
}