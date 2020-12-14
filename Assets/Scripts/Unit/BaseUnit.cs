using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Unit {
    [RequireComponent(typeof(Health))]
    public class BaseUnit : MonoBehaviour, IGetMaxHealth, IAction {
        [Header("Animation related")] [SerializeField]
        string animatorAttackTrigger;

        [SerializeField] protected Animator animator;

        [Header("Unit related")] [SerializeField]
        protected BasicUnit basicUnit;
        public GameObject bulletSpawnPoint;
        
        public GameObject target;
        public EquippedWeapon baseEquippedWeapon;
        protected NavMeshAgent BaseNavMeshAgent => GetComponent<NavMeshAgent>();
        protected Health BaseHealth => GetComponent<Health>();
        protected virtual bool EligibleToAttack { get; set; }

        protected float _attackTimer;

        void Awake() {
            Setup();
        }

        bool CanAttack => Time.time - _attackTimer > baseEquippedWeapon.weapon.attackSpeed;

        protected virtual void Update() {
            if (!EligibleToAttack) return;

            if (!CanAttack) return;

            CheckWeaponType();
            
            baseEquippedWeapon.weapon.Attack(transform, target);

            //TODO move  "_attackTimer = Time.time;" to StartAction? If so it means the "cooldown" will start once the animation is complete
            _attackTimer = Time.time;
            
            GetComponent<Action>().StartAction(this);
        }

        void CheckWeaponType() {
            if (basicUnit.rangedWeapon != null) {
                GetComponent<Action>().StartAction(GetComponent<RangedAttack>());
            }
            else {
                GetComponent<Action>().StartAction(GetComponent<MeleeAttack>());
            }
        }

        protected virtual GameObject CombatTarget {
            get => this.target = FindObjectOfType<PlayerController>().gameObject;
            set => target = value;
        } 

        protected void DeactivateAttack() {
            EligibleToAttack = false;
        }

        public int MaxHealth() {
            return basicUnit.maxHealth;
        }

        protected virtual void Setup() {
            BaseNavMeshAgent.speed = basicUnit.moveSpeed;
            BaseHealth.CurrentHealth = basicUnit.maxHealth;
            baseEquippedWeapon.ChangeWeapon(basicUnit.rangedWeapon);
        }

        public virtual void OnDeath() {
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;

            foreach (var script in GetComponents<MonoBehaviour>()) {
                script.enabled = false;
            }
        }

        public void ActionToStart() {
            //TODO something here?
        }
    }
}