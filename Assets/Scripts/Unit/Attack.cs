using UnityEngine;

namespace Unit {
    public class Attack : MonoBehaviour {
        public Weapon weapon;

        float _attackTimer;
        bool _canAttack;

        Bullet _bulletPrefab;
        [SerializeField] Transform bulletSpawnPoint;
        GameObject _target;
        Health _health;
        bool CanAttack => Time.time - this._attackTimer > this.weapon.attackSpeed;

        public void ChangeWeapon(Weapon weapon) {
            this.weapon = weapon;
            if (this.weapon is RangeWeapon rangeWeapon) {
                this._bulletPrefab = rangeWeapon.bulletPrefab;
            }
        }

        void Update() {
            if (!this._canAttack) return;

            if (!this.CanAttack) return;

            this._attackTimer = Time.time;
            if (this.weapon is RangeWeapon) {
                SpawnBullet();
            }
            else {
                Melee();
            }
        }

        //TODO: method that calls attack method based on current weapon.
        public void ActivateAttack(GameObject target) {
            //if(!this.CanAttack) return;
            if (this._canAttack) return;
            this._target = target;
            this._health = target.GetComponent<Health>();
            this._canAttack = true;
        }

        void SpawnBullet() {
            var bullet = Instantiate(this._bulletPrefab, this.bulletSpawnPoint.position, this.bulletSpawnPoint.rotation);
            bullet.Setup(this._target, this.weapon.baseDamage);
        }

        public void DeactivateAttack() {
            this._canAttack = false;
        }

        //TODO: Either use raycast, enable a gameobject or spherecast.
        void Melee() {
            this._health.TakeDamage(this.weapon.baseDamage);
        }

        //TODO: Fix attack delay on next attack!
    }
}