using UnityEngine;

namespace Unit.Enemy {
    public class BasicUnit : ScriptableObject {
        public int maxHealth;
        public float moveSpeed;
        public Weapon mainWeapon;
    }
}