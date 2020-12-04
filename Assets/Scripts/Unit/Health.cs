using System;
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
        public bool IsDead => this._health <= 0;

        public int MaxHealth { get; set; }


        void Start() {
            this._health = this.MaxHealth;
        }

        public void TakeDamage(int damage) {
            this._health -= damage;
            var damageUI = Instantiate(this.damageUIPrefab, this.parent.position,
                this.parent.rotation, this.parent);
            damageUI.SetUp(damage);
            if (this.IsDead) this.deathEvent?.Invoke();
            //StateLogic.CheckState();

            this.takingDamageEvent?.Invoke(this._health);
        }
    }
}