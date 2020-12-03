using System;
using UnityEngine;

namespace Unit {
    public class TemporaryWeaponScript : MonoBehaviour {
        public Weapon weapon;

        void Update() {
            if (Input.GetKeyDown(KeyCode.J)) {
                print(this.weapon.baseDamage);
            }
        }
    }
}