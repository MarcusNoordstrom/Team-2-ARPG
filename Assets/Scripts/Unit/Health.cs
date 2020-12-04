using System;
using GameStates;
using UnityEngine;
using UnityEngine.Events;

namespace Unit{
    [Serializable] public class FloatEvent : UnityEvent<float>{ }

    [Serializable] public class BoolEvent : UnityEvent{ }

    public class Health : MonoBehaviour{
        [SerializeField] private DamageUI damageUIPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private FloatEvent takingDamageEvent;
        [SerializeField] private BoolEvent deathEvent;
        private int health;
        private readonly int maxHealth = 100; //ScriptableObject: setup method or awake?
        public bool IsDead{ get => this.health <= 0; }

        private void Awake(){
            this.health = this.maxHealth;
        }
        public void TakeDamage(int damage){
            this.health -= damage;
            var damageUI = Instantiate(this.damageUIPrefab, this.parent.position,
                this.parent.rotation, this.parent);
            damageUI.SetUp(damage);
            if (this.IsDead){
                this.deathEvent?.Invoke();
                StateLogic.CheckState();
            }

            this.takingDamageEvent?.Invoke(this.health);
        }
        
    }
}