using UnityEngine;

namespace Unit {
    public class Weapon : ScriptableObject {
        public int baseDamage;
        public float attackSpeed;

        [Range(2, 50)] public int range = 4;
        //public GameObject weaponPrefab;
    }
}