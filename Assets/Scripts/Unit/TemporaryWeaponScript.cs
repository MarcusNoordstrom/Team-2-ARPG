﻿using UnityEngine;

namespace Unit {
    public class TemporaryWeaponScript : MonoBehaviour {
        public MeleeWeapon meleeWeapon;

        void Update() {
            if (Input.GetKeyDown(KeyCode.J)) print(meleeWeapon.baseDamage);
        }
    }
}