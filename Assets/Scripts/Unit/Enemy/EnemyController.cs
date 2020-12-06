using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Unit {
    [RequireComponent(typeof(VisibilityCheck), typeof(LookAtTarget), typeof(Attack))]
    [RequireComponent(typeof(NavMeshAgent), typeof(Health))]
    public class EnemyController : MonoBehaviour {
        const int TicksPerUpdate = 15;

        [SerializeField] BasicEnemy basicEnemy;
        State _state;
        Attack _attack;
        LookAtTarget _lookAtTarget;
        NavMeshAgent _navmeshAgent;
        Vector3 _roamPosition;
        Vector3 _startingPosition;
        PlayerController _target;
        int _ticks;
        VisibilityCheck _visibilityCheck;

        void Awake() {
            this._navmeshAgent = GetComponent<NavMeshAgent>();
            this._state = State.Roaming;
            this._lookAtTarget = GetComponent<LookAtTarget>();
            this._attack = GetComponent<Attack>();
            this._visibilityCheck = GetComponent<VisibilityCheck>();
            SetupEnemy();
            this._attack.ChangeWeapon(this.basicEnemy.mainWeapon);
        }

        void Start() {
            this._target = FindObjectOfType<PlayerController>();
            this._lookAtTarget.Setup(this._target.transform);
            this._startingPosition = this.transform.position;
            this._roamPosition = GetRoamingPosition();
            this._ticks = Random.Range(0, TicksPerUpdate);
        }

        void FixedUpdate() {
            this._ticks++;
            if (this._ticks < TicksPerUpdate)
                return;
            this._ticks -= TicksPerUpdate;

            switch (this._state) {
                case State.Roaming:
                    RoamToNewPosition();
                    break;
                case State.ChaseTarget:
                    ChaseTarget();

                    break;
                case State.GoingBackToStart:
                    GoingBackToStart();
                    break;
            }
        }

        // void OnDrawGizmosSelected() {
        //     Gizmos.color = Color.blue;
        //     Gizmos.DrawWireSphere(this.transform.position, this.basicEnemy.targetRange);
        // }

        void SetupEnemy() {
            GetComponent<Health>().MaxHealth = this.basicEnemy.maxHealth;
            this._navmeshAgent.speed = this.basicEnemy.moveSpeed;
        }

        void GoingBackToStart() {
            this._navmeshAgent.SetDestination(this._startingPosition);
            if (Vector3.Distance(this.transform.position, this._startingPosition) < 1f) {
                this._state = State.Roaming;
                return;
            }

            FindTarget();
        }

        void ChaseTarget() {
            if (!this._visibilityCheck.IsVisible(this._target.gameObject)) {
                this._lookAtTarget.enabled = false;
                this._navmeshAgent.isStopped = false;
                this._attack.DeactivateAttack();
                this._state = State.GoingBackToStart;
                return;
            }

            this._navmeshAgent.SetDestination(this._target.transform.position);
            if (Vector3.Distance(this.transform.position, this._target.transform.position) < this._attack.weapon.range) {
                this._navmeshAgent.isStopped = true;
                this._lookAtTarget.enabled = true;
                if (Vector3.Angle(this.transform.forward,
                    (this._target.transform.position - this.transform.position).normalized) < 50)
                    this._attack.ActivateAttack(this._target.gameObject);
            }
            else {
                this._navmeshAgent.isStopped = false;
                this._attack.DeactivateAttack();
            }

            if (Vector3.Distance(this.transform.position, this._target.transform.position) > this.basicEnemy.stopChaseDistance) {
                this._lookAtTarget.enabled = false;
                this._state = State.GoingBackToStart;
            }
        }

        void RoamToNewPosition() {
            this._navmeshAgent.SetDestination(this._roamPosition);
            if (Vector3.Distance(this.transform.position, this._roamPosition) < 1f)
                this._roamPosition = GetRoamingPosition();
            FindTarget();
        }

        static Vector3 GetRandomDir() {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        Vector3 GetRoamingPosition() {
            return this._startingPosition + GetRandomDir() * Random.Range(3f, 10f);
        }

        void FindTarget() {
            if (Vector3.Distance(this.transform.position, this._target.transform.position) < this.basicEnemy.targetRange)
                if (this._visibilityCheck.IsVisible(this._target.gameObject)) {
                    this._roamPosition = GetRoamingPosition();
                    this._state = State.ChaseTarget;
                }
        }

        enum State {
            Roaming,
            ChaseTarget,
            GoingBackToStart
        }
    }
}