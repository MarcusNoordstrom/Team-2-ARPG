using UnityEngine;
using UnityEngine.AI;

namespace Unit {
    [RequireComponent(typeof(NavMeshAgent), typeof(Health), typeof(Attack))]
    public class BaseUnit : MonoBehaviour, IGetMaxHealth {
        [SerializeField] private protected BasicUnit basicUnit;
        protected NavMeshAgent BaseNavMeshAgent => GetComponent<NavMeshAgent>();
        protected Attack BaseAttack => GetComponent<Attack>();
        protected Health BaseHealth => GetComponent<Health>();

        void Awake() {
            Setup();
        }

        public int MaxHealth() {
            return basicUnit.maxHealth;
        }

        void Setup() {
            GetComponent<NavMeshAgent>().speed = basicUnit.moveSpeed;
            GetComponent<Health>().CurrentHealth = basicUnit.maxHealth;
            BaseAttack.ChangeWeapon(basicUnit.mainWeapon);
            BaseHealth.CurrentHealth = basicUnit.maxHealth;
        }

        public virtual void OnDeath() {
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
        }
    }
}