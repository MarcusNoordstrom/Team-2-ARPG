﻿using UnityEngine;

namespace Unit {
    public class Attack : MonoBehaviour {
        [HideInInspector]
        public Weapon weapon;
        [SerializeField] Transform bulletSpawnPoint;

        float _attackTimer;

        public string animTrigger;

        Bullet _bulletPrefab;
        bool _canAttack;
        Health _health;
        GameObject _target;
        bool CanAttack => Time.time - _attackTimer > weapon.attackSpeed;

        void Update() {
            if (!_canAttack) return;

            if (!CanAttack) return;
            
            _attackTimer = Time.time;
            if (weapon is RangeWeapon)
                SpawnBullet();
            else
                Melee();
            if (animTrigger == null || GetComponent<Animator>() == null) return;
            GetComponent<Animator>().SetTrigger(animTrigger);
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

        void SpawnBullet() {
            var bullet = Instantiate(_bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
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