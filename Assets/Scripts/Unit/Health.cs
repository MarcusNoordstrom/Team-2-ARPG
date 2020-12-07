using System;
using GameStates;
using UnityEngine;
using UnityEngine.Events;

namespace Unit {
    [Serializable]
    public class FloatEvent : UnityEvent<float> {
    }

    [Serializable]
    public class BoolEvent : UnityEvent {
    }

    public class Health : MonoBehaviour {
        [SerializeField] DamageUI damageUIPrefab;
        [SerializeField] Transform canvasParent;
        [SerializeField] FloatEvent takingDamageEvent;
        [SerializeField] BoolEvent deathEvent;
        [SerializeField] BoolEvent reviveEvent;

        int _currentCurrentHealth;

        public int CurrentHealth {
            get => _currentCurrentHealth;
            set => _currentCurrentHealth =
                Mathf.Clamp(value, 0, GetComponent<IGetMaxHealth>().MaxHealth());
        }

        public bool IsDead => CurrentHealth <= 0;

        public void TakeDamage(int damage) {
            CurrentHealth -= damage;
            var damageUI = Instantiate(damageUIPrefab, canvasParent.position,
                canvasParent.rotation, canvasParent);
            damageUI.SetUp(damage);
            if (IsDead) deathEvent?.Invoke();

            //Enters dead state.
            takingDamageEvent?.Invoke(CurrentHealth);
            //TODO: Change hard coded layer index to -> something not hard coded.
            if (gameObject.layer != 8 && !IsDead) return;
            StateLogic.CheckState();
        }

        public void RevivePlayer() {
            reviveEvent?.Invoke();
        }
    }
}