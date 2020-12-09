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
        [SerializeField] BoolEvent lowHealthEvent;
        [SerializeField] float lowHealthTrigger;

        int _currentCurrentHealth;
        bool soundTriggered;

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
            takingDamageEvent?.Invoke(CurrentHealth);
            
            if (CurrentHealth <= lowHealthTrigger && !soundTriggered) {
                soundTriggered = true;
                lowHealthEvent?.Invoke();
            }

            if (IsDead) deathEvent?.Invoke();
        }

        public void RevivePlayer() { //TODO Convert to interface
            soundTriggered = false;
            takingDamageEvent?.Invoke(CurrentHealth);
        }
    }
}