using Player;
using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(VisibilityCheck), typeof(LookAtTarget))]
    
    public class EnemyController : BaseUnit {
        const int TicksPerUpdate = 15;

        private BasicEnemy _basicEnemy;
        
        State _state;
        LookAtTarget _lookAtTarget;
        Vector3 _roamPosition;
        Vector3 _startingPosition;
        PlayerController _target;
        int _ticks;
        VisibilityCheck _visibilityCheck;

        void Start() {
            _basicEnemy = (BasicEnemy) basicUnit;
            this._state = State.Roaming;
            this._lookAtTarget = GetComponent<LookAtTarget>();
            this._visibilityCheck = GetComponent<VisibilityCheck>();
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

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(this.transform.position, _basicEnemy.targetRange);
        }

        void GoingBackToStart() {
            BaseNavMeshAgent.SetDestination(this._startingPosition);
            if (Vector3.Distance(this.transform.position, this._startingPosition) < 1f) {
                this._state = State.Roaming;
                return;
            }
            FindTarget();
        }

        void ChaseTarget() {
            if (!this._visibilityCheck.IsVisible(this._target.gameObject)) {
                this._lookAtTarget.enabled = false;
                BaseNavMeshAgent.isStopped = false;
                BaseAttack.DeactivateAttack();
                this._state = State.GoingBackToStart;
                return;
            }

            BaseNavMeshAgent.SetDestination(this._target.transform.position);
            if (Vector3.Distance(this.transform.position, this._target.transform.position) < BaseAttack.weapon.range) {
                BaseNavMeshAgent.isStopped = true;
                this._lookAtTarget.enabled = true;
                if (Vector3.Angle(this.transform.forward,
                    (this._target.transform.position - this.transform.position).normalized) < 50)
                    BaseAttack.ActivateAttack(this._target.gameObject);
            }
            else {
                BaseNavMeshAgent.isStopped = false;
                BaseAttack.DeactivateAttack();
            }

            if (Vector3.Distance(this.transform.position, this._target.transform.position) > _basicEnemy.stopChaseDistance) {
                this._lookAtTarget.enabled = false;
                this._state = State.GoingBackToStart;
            }
        }

        void RoamToNewPosition() {
            BaseNavMeshAgent.SetDestination(this._roamPosition);
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
            if (Vector3.Distance(this.transform.position, this._target.transform.position) < _basicEnemy.targetRange)
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