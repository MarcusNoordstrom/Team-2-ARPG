using UnityEngine;

namespace Unit {
    public class BasicUnit : ScriptableObject {
        //Todo add game object for weapon prefab
        public int maxHealth;
        public float moveSpeed;
        public Weapon mainWeapon;
    }
}