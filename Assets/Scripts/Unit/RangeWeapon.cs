using UnityEngine;

namespace Unit {
    [CreateAssetMenu(fileName = "Unnamed Ranged Weapon", menuName = "Weapon/Ranged")]
    public class RangeWeapon : Weapon, IRange {
        public Bullet bulletPrefab;
        
        public Bullet BulletPrefab() {
            return bulletPrefab;
        }

        public override void Attack(Transform transform, GameObject target) {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
            bullet.BulletFiredBy = LayerMask.GetMask();
            bullet.Setup(target, baseDamage);
        }
    }
}