using UnityEngine;
using UnityEngine.AI;

namespace Unit {
    public class BaseUnit : MonoBehaviour, IGetMaxHealth {
        [SerializeField] protected BasicUnit basicUnit;
        [SerializeField] protected Animator animator;
        protected NavMeshAgent BaseNavMeshAgent => GetComponent<NavMeshAgent>();
        public Attack BaseAttack;
        protected Health BaseHealth => GetComponent<Health>();
        public GameObject target;

        void Awake() {
            Setup();
        }
        
        void Update() {
            if (!BaseAttack._canAttack) return;

            if (!BaseAttack.CanAttack) return;

            if (BaseAttack.animTrigger != null || animator != null) {
                animator.SetTrigger("Shoot");
            }
            BaseAttack.weapon.Attack(this.transform, BaseAttack._target);
            
            BaseAttack._attackTimer = Time.time;
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
            GetComponent<NavMeshObstacle>().enabled = false;
            
            foreach (var script in GetComponents<MonoBehaviour>()) {
                script.enabled = false;
            }
        }
    }
}