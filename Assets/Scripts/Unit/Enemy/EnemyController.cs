using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unit {
    [RequireComponent(typeof(VisibilityCheck), typeof(LookAtTarget))]
    public class EnemyController : BaseUnit, IAction {
        [SerializeField] float waitAtWaypoint = 5;
        const int TicksPerUpdate = 15;
        BasicEnemy BasicEnemy => (BasicEnemy) basicUnit;
        LookAtTarget LookAtTarget => GetComponent<LookAtTarget>();
        Vector3 StartingPosition => transform.position;
        State _state;
        int _ticks;
        VisibilityCheck _visibilityCheck;

        public GameObject wayPointObject;

        readonly List<Transform> _waypoints = new List<Transform>();
        bool _isPatrolling;
        int _x;
        float _timer;

        bool WaitTimer => Time.time - waitAtWaypoint > _timer;

        void Start() {
            foreach (Transform child in wayPointObject.GetComponentInChildren<Transform>()) {
                if (child.gameObject == wayPointObject) continue;
                _waypoints.Add(child);
            }

            Patrol();
            _visibilityCheck = GetComponent<VisibilityCheck>();
            _state = State.Patrolling;
            LookAtTarget.Setup(CombatTarget.transform);
            _ticks = Random.Range(0, TicksPerUpdate);
        }

        void FixedUpdate() {
            if (ReachedPosition()) {
                if (WaitTimer) {
                    _x++;
                    if (_x == _waypoints.Count) {
                        _x = 0;
                    }

                    _isPatrolling = true;
                    _timer = Time.time;
                }
            }

            _ticks++;
            if (_ticks < TicksPerUpdate)
                return;
            _ticks -= TicksPerUpdate;

            switch (_state) {
                case State.Patrolling:
                    FindTarget();
                    if (_isPatrolling)
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

        bool ReachedPosition() {
            return _waypoints[_x].position.z == transform.position.z &&
                   _waypoints[_x].position.x == transform.position.x;
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, BasicEnemy.targetRange);
        }

        void OnDrawGizmos() {
            var children = wayPointObject.GetComponentsInChildren<Transform>();

            for (var i = 0; i < children.Length; i++) {
                if (i == 0) continue;
                Gizmos.color = Color.red;

                Gizmos.DrawSphere(children[i].transform.position, .3f);

                if (i < children.Length - 1) {
                    Gizmos.DrawLine(children[i].transform.position, children[i + 1].transform.position);

                    continue;
                }

                Gizmos.DrawLine(children[i].transform.position, children[1].transform.position);
            }
        }

        void GoingBackToStart() {
            BaseNavMeshAgent.SetDestination(StartingPosition);
            if (Vector3.Distance(transform.position, StartingPosition) < 1f) {
                _state = State.Patrolling;
                _isPatrolling = true;
                return;
            }

            FindTarget();
        }

        void ChaseTarget() {
            if (!_visibilityCheck.IsVisible(CombatTarget.gameObject)) {
                LookAtTarget.enabled = false;
                BaseNavMeshAgent.isStopped = false;
                DeactivateAttack();
                _state = State.GoingBackToStart;
                return;
            }

            BaseNavMeshAgent.SetDestination(CombatTarget.transform.position);
            if (Vector3.Distance(transform.position, CombatTarget.transform.position) < baseEquippedWeapon.weapon.range) {
                BaseNavMeshAgent.isStopped = true;
                LookAtTarget.enabled = true;
                if (Vector3.Angle(transform.forward,
                    (CombatTarget.transform.position - transform.position).normalized) < 50)
                    return;
                //BaseAttack.ActivateAttack(_target.gameObject);
            }
            else {
                BaseNavMeshAgent.isStopped = false;
                DeactivateAttack();
            }

            if (Vector3.Distance(transform.position, CombatTarget.transform.position) > BasicEnemy.stopChaseDistance) {
                LookAtTarget.enabled = false;
                _state = State.GoingBackToStart;
            }
        }

        void Patrol() {
            _isPatrolling = false;
            BaseNavMeshAgent.SetDestination(_waypoints[_x].position);

            FindTarget();
        }


        void FindTarget() {
            if (Vector3.Distance(transform.position, CombatTarget.transform.position) < BasicEnemy.targetRange)
                if (_visibilityCheck.IsVisible(CombatTarget.gameObject)) {
                    _state = State.ChaseTarget;
                }
        }

        enum State {
            Patrolling,
            ChaseTarget,
            GoingBackToStart
        }

        public void ActionToStart() {
            //TODO should return to his position here
        }
    }
}