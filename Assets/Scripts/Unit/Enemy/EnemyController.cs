using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;

namespace Unit {
    [RequireComponent(typeof(VisibilityCheck), typeof(LookAtTarget))]
    public class EnemyController : BaseUnit {
        const int TicksPerUpdate = 15;
        BasicEnemy BasicEnemy => (BasicEnemy) basicUnit;
        LookAtTarget LookAtTarget => GetComponent<LookAtTarget>();
        Vector3 _roamPosition;
        Vector3 StartingPosition => transform.position;
        State _state;
        PlayerController _target => FindObjectOfType<PlayerController>();
        int _ticks;
        VisibilityCheck _visibilityCheck;
        public Transform[] waypoints;
        bool isPatrolling;
        int x = 0;
        float timer = 0f;
        private float waitAtWaypoint = 3;
        private bool WaitTimer => Time.time - waitAtWaypoint > timer;

        void Start() {
            Patrol();
            _visibilityCheck = GetComponent<VisibilityCheck>();
            _state = State.Patrolling;
            LookAtTarget.Setup(_target.transform);
            _ticks = Random.Range(0, TicksPerUpdate);
        }

        void FixedUpdate() {
            if (ReachedPosition()) {
                if (WaitTimer) {
                    print(timer);
                    x++;
                    if (x == waypoints.Length) {
                        x = 0;
                    }

                    isPatrolling = true;
                    timer = Time.time;
                }
            }

            _ticks++;
            if (_ticks < TicksPerUpdate)
                return;
            _ticks -= TicksPerUpdate;

            switch (_state) {
                case State.Patrolling:
                    FindTarget();
                    if (isPatrolling)
                        Patrol();
                    break;
                case State.ChaseTarget:
                    ChaseTarget();

                    break;
                case State.GoingBackToStart:
                    GoingBackToStart();
                    break;
            }
        }

        private bool ReachedPosition() {
            return waypoints[x].position.z == transform.position.z &&
                   waypoints[x].position.x == transform.position.x;
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, BasicEnemy.targetRange);
        }

        void GoingBackToStart() {
            BaseNavMeshAgent.SetDestination(StartingPosition);
            if (Vector3.Distance(transform.position, StartingPosition) < 1f) {
                _state = State.Patrolling;
                return;
            }

            FindTarget();
        }

        void ChaseTarget() {
            if (!_visibilityCheck.IsVisible(_target.gameObject)) {
                LookAtTarget.enabled = false;
                BaseNavMeshAgent.isStopped = false;
                BaseAttack.DeactivateAttack();
                _state = State.GoingBackToStart;
                return;
            }

            BaseNavMeshAgent.SetDestination(_target.transform.position);
            if (Vector3.Distance(transform.position, _target.transform.position) < BaseAttack.weapon.range) {
                BaseNavMeshAgent.isStopped = true;
                LookAtTarget.enabled = true;
                if (Vector3.Angle(transform.forward,
                    (_target.transform.position - transform.position).normalized) < 50)
                    BaseAttack.ActivateAttack(_target.gameObject);
            }
            else {
                BaseNavMeshAgent.isStopped = false;
                BaseAttack.DeactivateAttack();
            }

            if (Vector3.Distance(transform.position, _target.transform.position) > BasicEnemy.stopChaseDistance) {
                LookAtTarget.enabled = false;
                _state = State.GoingBackToStart;
            }
        }

        void Patrol() {
            isPatrolling = false;
            BaseNavMeshAgent.SetDestination(waypoints[x].position);

            FindTarget();
        }


        void FindTarget() {
            if (Vector3.Distance(transform.position, _target.transform.position) < BasicEnemy.targetRange)
                if (_visibilityCheck.IsVisible(_target.gameObject)) {
                    _state = State.ChaseTarget;
                }
        }

        enum State {
            Patrolling,
            ChaseTarget,
            GoingBackToStart
        }
    }
}