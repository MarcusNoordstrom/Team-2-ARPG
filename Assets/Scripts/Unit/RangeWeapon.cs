using UnityEngine;

namespace Unit {
    [CreateAssetMenu(fileName = "Unnamed Ranged Weapon", menuName = "Weapon/Ranged")]
    public class RangeWeapon : Weapon, IRange {
        public Bullet bulletPrefab;
        
        public Bullet BulletPrefab() {
            return bulletPrefab;
        }
    }
}