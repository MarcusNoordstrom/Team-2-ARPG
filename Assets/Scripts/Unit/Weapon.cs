using UnityEngine;

namespace Unit {
    [CreateAssetMenu]
    public class Weapon : ScriptableObject {
        public WeaponType weaponType;
        public int baseDamage;
    }

    public enum WeaponType {
        Melee,
        Ranged
    }
}