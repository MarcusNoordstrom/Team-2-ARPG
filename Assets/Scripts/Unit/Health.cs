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

    public class Health : MonoBehaviour, IResurrect {
        [SerializeField] FloatEvent takingDamageEvent;
        [SerializeField] BoolEvent deathEvent;

        public static int CurrentHealthBars;

        int _currentCurrentHealth;


        public delegate void UpdateHealthUI(int damage);

        public static UpdateHealthUI UpdatePlayerHealthUI;

        public int CurrentHealth {
            get => _currentCurrentHealth;
            set => _currentCurrentHealth =
                Mathf.Clamp(value, 0, GetComponent<IGetMaxHealth>().MaxHealth());
        }

        public bool IsDead => CurrentHealth <= 0;

        public void TakeDamage(int damage) {
            CurrentHealth -= damage;

            if (IsDead) {
                deathEvent?.Invoke();
                GetComponent<Collider>().enabled = false;
            }
            
            UpdatePlayerHealthUI(damage);
            
        }

        public void OnResurrect(bool onCorpse) {
            takingDamageEvent?.Invoke(CurrentHealth);
            GetComponent<Collider>().enabled = true;
        }
    }
}