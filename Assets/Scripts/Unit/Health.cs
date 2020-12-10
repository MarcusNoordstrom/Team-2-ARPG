using System;
using UI;
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
        [SerializeField] GameObject HealthUI;
        [SerializeField] DamageUI damageUIPrefab;
        [SerializeField] Transform canvasParent;
        [SerializeField] FloatEvent takingDamageEvent;
        [SerializeField] BoolEvent deathEvent;
        [SerializeField] BoolEvent lowHealthEvent;
        [SerializeField] float lowHealthTrigger;
        
        public static int CurrentHealthBars;

        int _currentCurrentHealth;
        bool soundTriggered;

        public int CurrentHealth {
            get => _currentCurrentHealth;
            set => _currentCurrentHealth =
                Mathf.Clamp(value, 0, GetComponent<IGetMaxHealth>().MaxHealth());
        }

        void Start() {
            if (gameObject.layer == LayerMask.NameToLayer("Player")) {
                CurrentHealthBars = CurrentHealth; 
                HealthUI.GetComponent<HealthBarUI>().InstantiateHealthTicks();
            }
        }

        public bool IsDead => CurrentHealth <= 0;
        
        public void TakeDamage(int damage) {
            CurrentHealth -= damage;
            var damageUI = Instantiate(damageUIPrefab, canvasParent.position,
                canvasParent.rotation, canvasParent);
            damageUI.SetUp(damage);
            //takingDamageEvent?.Invoke(CurrentHealth * 0.5f);
            
            HealthUI.GetComponent<HealthBarUI>().RemoveHealthTick(damage);
            
            Debug.Log(CurrentHealth);
            
            if (CurrentHealth <= lowHealthTrigger && !soundTriggered) {
                soundTriggered = true;
                lowHealthEvent?.Invoke();
            }

            if (IsDead) deathEvent?.Invoke();
        }

        public void RevivePlayer() { //TODO Convert to interface
            soundTriggered = false;
            takingDamageEvent?.Invoke(CurrentHealth);
            HealthUI.GetComponent<HealthBarUI>().InstantiateHealthTicks();
        }
    }
}