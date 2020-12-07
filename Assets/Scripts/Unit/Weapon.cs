using UnityEngine;

namespace Unit {
    public class Weapon : ScriptableObject {
        public int baseDamage;
        public float attackSpeed;
        [Range(2, 50)]public int range;
        //public GameObject weaponPrefab;
    }
}