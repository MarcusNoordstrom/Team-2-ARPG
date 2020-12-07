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
        public bool IsDead => this.CurrentHealth <= 0;

        public void TakeDamage(int damage) {
            this.CurrentHealth -= damage;
            var damageUI = Instantiate(this.damageUIPrefab, this.canvasParent.position,
                this.canvasParent.rotation, this.canvasParent);
            damageUI.SetUp(damage);
            if (this.IsDead) this.deathEvent?.Invoke();
            
            //Enters dead state.
            this.takingDamageEvent?.Invoke(this.CurrentHealth);
            //TODO: Change hard coded layer index to -> something not hard coded.
            if (this.gameObject.layer != 8 && !this.IsDead) return;
            StateLogic.CheckState();
        }

        public void RevivePlayer() {
           
            reviveEvent?.Invoke();
        }
    }
}