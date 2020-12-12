using UnityEngine;

namespace Unit {
    public class Attack : MonoBehaviour {
        public Weapon weapon;
        [SerializeField] Transform bulletSpawnPoint;
        public string animTrigger;
        [SerializeField] private Animator animator;
        
        float _attackTimer;
        Bullet _bulletPrefab;
        bool _canAttack;
        Health _health;
        GameObject _target;
        bool CanAttack => Time.time - _attackTimer > weapon.attackSpeed;

        void Update() {
            if (!_canAttack) return;

            if (!CanAttack) return;
            
            if (animTrigger == null || this.animator == null) return;
            this.animator.SetTrigger(animTrigger);
            
            if (weapon is RangeWeapon)
                SpawnBullet();
            else
                Melee();
            _attackTimer = Time.time;
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