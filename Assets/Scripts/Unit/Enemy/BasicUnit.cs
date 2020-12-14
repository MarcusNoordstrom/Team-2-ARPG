using UnityEngine;

namespace Unit {
    public class BasicUnit : ScriptableObject {
        public int maxHealth;
        public float moveSpeed;
        public Weapon rangedWeapon;
        public MeleeWeapon meleeWeapon;

    }

    public interface IGetMaxHealth {
        int MaxHealth();
    }
}