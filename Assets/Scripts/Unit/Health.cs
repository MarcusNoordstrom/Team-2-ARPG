using System;
using UnityEngine;
using UnityEngine.Events;

namespace Unit{
    [Serializable] public class FloatEvent : UnityEvent<float>{ }

    [Serializable] public class BoolEvent : UnityEvent{ }

    public class Health : MonoBehaviour{
        [SerializeField] private GameObject damageUIPrefab;
        [SerializeField] private Transform parent;
        [SerializeField] private FloatEvent takingDamageEvent;
        [SerializeField] private BoolEvent deathEvent;
        private int health;
        private readonly int maxHealth = 100; //ScriptableObject: setup method or awake?
        public bool IsDead{ get; private set; }

        private void Awake(){
            this.health = this.maxHealth;
        }
        public void TakeDamage(int damage){
            this.health -= damage;
            var damageUI = Instantiate(this.damageUIPrefab, this.parent.position,
                this.parent.rotation, this.parent);
            damageUI.GetComponent<DamageUI>().SetUp(damage);
            if (this.health <= 0){
                IsDead = true;
                this.deathEvent?.Invoke();
            }

            this.takingDamageEvent?.Invoke(this.health);
        }
        
    }
}