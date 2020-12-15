using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace Unit {
    [RequireComponent(typeof(Health))]
    public class BaseUnit : MonoBehaviour, IGetMaxHealth, IAction {
        [Header("Animation related")] [SerializeField]
        protected Animator animator;

        [Header("Unit related")] [SerializeField]
        protected BasicUnit basicUnit;

        public GameObject bulletSpawnPoint;
        [SerializeField] GameObject target;
        [FormerlySerializedAs("baseEquippedWeapon")] public EquippedWeapon equipped;
        protected NavMeshAgent BaseNavMeshAgent => GetComponent<NavMeshAgent>();
        protected Health BaseHealth => GetComponent<Health>();
        protected virtual bool EligibleToAttack { get; set; }

        public float _attackTimer;


        void Awake() {
            BaseHealth.DeadStuff += OnDeath;
            Setup();
        }

        public bool CanAttack => Time.time - _attackTimer > equipped.weapon.attackSpeed;

        protected virtual void Update() {
            if (!EligibleToAttack) return;

            if (!CanAttack) return;

            CheckWeaponType();

            equipped.weapon.Attack(transform, target);

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

        public virtual GameObject CombatTarget {
            get => target;
            protected set => target = value;
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
            equipped.ChangeWeapon(basicUnit.rangedWeapon);
        }

        public virtual void OnDeath() {
            print(BaseHealth.CurrentHealth);
            GetComponent<Collider>().enabled = false;
            GetComponent<NavMeshObstacle>().enabled = false;
            animator.SetTrigger("TurretDeath");

            foreach (var script in GetComponents<MonoBehaviour>()) {
                script.enabled = false;
            }
        }

        public void ActionToStart() {
            print("Base unit");
            //TODO something here?
        }
    }
}