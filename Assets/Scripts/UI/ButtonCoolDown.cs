using System;
using System.Collections;
using Player;
using Unit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI {
    public class ButtonCoolDown : MonoBehaviour {
        BaseUnit _unit;
        public Image rangeAttackImage;
        public Image meleeAttackImage;

        public static UnityAction RangedCoolDownAction;
        public static UnityAction MeleeCoolDownAction;

        public static bool meleeStartFilling;
        public static bool rangeStartFilling;

        void Start() {
            _unit = FindObjectOfType<PlayerController>().GetComponent<BaseUnit>();
            rangeAttackImage.fillAmount = 1;
            meleeAttackImage.fillAmount = 1;
        }

        void Update() {

            if (rangeStartFilling) {
                RangedCoolDown(_unit.basicUnit.rangedWeapon.attackSpeed);
            }

            if (meleeStartFilling) {
                MeleeCoolDown(_unit.basicUnit.meleeWeapon.attackSpeed);
            }
        }

        void MeleeCoolDown(float cooldownTimer) {
            var timer = 0f;
            timer += Time.deltaTime;
            meleeAttackImage.fillAmount += timer / cooldownTimer;

            if (meleeAttackImage.fillAmount >= 1) meleeStartFilling = false;
        }

        void RangedCoolDown(float cooldownTimer) {
            var timer = 0f;
            timer += Time.deltaTime;
            rangeAttackImage.fillAmount += timer / cooldownTimer;

            if (rangeAttackImage.fillAmount >= 1) rangeStartFilling = false;
        }
    }
}