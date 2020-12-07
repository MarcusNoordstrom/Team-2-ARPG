using UnityEngine;

namespace Unit {
    public class BasicUnit : ScriptableObject {
        public int maxHealth;
        public float moveSpeed;
        public Weapon mainWeapon;
    }

    public interface IGetMaxHealth {
        int MaxHealth();
    }
}