using System;
using UnityEngine;
using UnityEngine.VFX;

namespace Unit {
    [Serializable]
    public class EquippedWeapon {
        public Weapon weapon;
        

        
        //TODO move visual effect to weapon SO
        //public VisualEffect visualEffect;
        

        //TODO rename the animation event to "EnemyAttacking"?
        //animation event
        void TurretShooting() {
            //visualEffect.Play();
        }

        public void ChangeWeapon(Weapon weapon) {
            this.weapon = weapon;
        }

        //TODO: method that calls attack method based on current weapon.
        // public void ActivateAttack(GameObject target) {
        //     //if(!this.CanAttack) return;
        //     if (canAttack) return;
        //     canAttack = true;
        // }
        //


        //TODO: Fix attack delay on next attack!
    }
}