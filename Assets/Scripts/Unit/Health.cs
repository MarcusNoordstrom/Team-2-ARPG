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
        [SerializeField] Transform parent;
        [SerializeField] FloatEvent takingDamageEvent;
        [SerializeField] BoolEvent deathEvent;
        int _health;
        public bool IsDead {
            get { return this._health <= 0; }
            set{}
        }

        public int MaxHealth { get; set; }


        void Start() {
            if (MaxHealth <= 0) {
                this.MaxHealth = 100;
            }
            this._health = this.MaxHealth;
        }

        public void TakeDamage(int damage) {
            this._health -= damage;
            var damageUI = Instantiate(this.damageUIPrefab, this.parent.position,
                this.parent.rotation, this.parent);
            damageUI.SetUp(damage);
            if (this.IsDead) this.deathEvent?.Invoke();
            
            //Enters dead state.
            this.takingDamageEvent?.Invoke(this._health);
            //TODO: Change hard coded layer index to -> something not hard coded.
            if (this.gameObject.layer != 8 && !this.IsDead) return;
            StateLogic.CheckState();
        }
    }
}