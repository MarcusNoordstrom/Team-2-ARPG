using System;
using UnityEngine;

namespace Unit {
    [CreateAssetMenu]
    public class Weapon : ScriptableObject {
        public WeaponType weaponType;
        public int baseDamage;
        public float attackSpeed;
        public int range;
        public GameObject weaponPrefab;
        
    }
    

    public enum WeaponType {
        Melee,
        Ranged
    }
}