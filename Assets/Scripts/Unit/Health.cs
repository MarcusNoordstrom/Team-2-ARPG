﻿using System;
using GameStates;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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

        //subscribed to in PlayerController.cs
        public static UpdateHealthUI UpdatePlayerHealthUI;

        public int CurrentHealth {
            get => _currentCurrentHealth;
            set => _currentCurrentHealth =
                Mathf.Clamp(value, 0, GetComponent<IGetMaxHealth>().MaxHealth());
        }


        void Awake() {
            if (gameObject.layer == LayerMask.NameToLayer("Player")) {
                CurrentHealthBars = CurrentHealth;
            }
        }

        public bool IsDead => CurrentHealth <= 0;

        public void TakeDamage(int damage) {
            CurrentHealth -= damage;

            UpdatePlayerHealthUI(damage);
            if (IsDead) deathEvent?.Invoke();
        }

        public void OnResurrect(bool onCorpse) {
            takingDamageEvent?.Invoke(CurrentHealth);
        }
    }
}