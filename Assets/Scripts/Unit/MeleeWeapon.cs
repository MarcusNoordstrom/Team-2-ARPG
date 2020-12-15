using UnityEngine;

namespace Unit {
    [CreateAssetMenu(fileName = "Unnamed Melee Weapon", menuName = "Weapon/Melee")]
    public class MeleeWeapon : Weapon {
        public override void Attack(Transform transform, GameObject target) {
            throw new System.NotImplementedException();
        }
    }
}