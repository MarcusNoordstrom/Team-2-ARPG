﻿using UnityEngine;
using UnityEngine.AI;

namespace Unit {
    [RequireComponent(typeof(Health), typeof(Attack))]
    public class BaseUnit : MonoBehaviour, IGetMaxHealth {
        [SerializeField] protected BasicUnit basicUnit;
        [SerializeField] protected Animator animator;
        protected NavMeshAgent BaseNavMeshAgent => GetComponent<NavMeshAgent>();
        protected Attack BaseAttack => GetComponent<Attack>();
        protected Health BaseHealth => GetComponent<Health>();

        void Awake() {
            Setup();
        }

        public int MaxHealth() {
            return basicUnit.maxHealth;
        }

        protected virtual void Setup() {
            BaseNavMeshAgent.speed = basicUnit.moveSpeed;
            BaseHealth.CurrentHealth = basicUnit.maxHealth;
            BaseAttack.ChangeWeapon(basicUnit.mainWeapon);
        }

        public virtual void OnDeath() {
            GetComponent<Collider>().enabled = false;
            foreach (var script in GetComponents<MonoBehaviour>()) {
                script.enabled = false;
            }
        }
    }
}