﻿using System;
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
        private Vector3 StartingPosition;
        State _state;
        int _ticks;
        VisibilityCheck _visibilityCheck;

        public GameObject wayPointObject;

        readonly List<Transform> _waypoints = new List<Transform>();
        bool _isPatrolling;
        int _x;
        float _timer;
        bool _shouldIdle;
        public bool patrollingUnit;

        bool WaitTimer => Time.time - _timer > waitAtWaypoint;

        void Start() {
            CombatTarget = FindObjectOfType<PlayerController>().gameObject;
            foreach (Transform child in wayPointObject.GetComponentInChildren<Transform>()) {
                if (child.gameObject == wayPointObject) continue;
                _waypoints.Add(child);
                StartingPosition = _waypoints[0].position;
            }

            Patrol();
            _visibilityCheck = GetComponent<VisibilityCheck>();
            _state = State.Patrolling;
            LookAtTarget.Setup(CombatTarget.transform);
            _ticks = Random.Range(0, TicksPerUpdate);
        }

        void FixedUpdate() {
            if (ReachedPosition()) {
                if (!_shouldIdle) {
                    _timer = Time.time;
                    animator.SetTrigger("DroneIdle");
                    _shouldIdle = true;
                }

                if (WaitTimer) {
                    _x++;
                    if (_x == _waypoints.Count) {
                        _x = 0;
                    }

                    _shouldIdle = false;
                    _isPatrolling = true;
                }
            }

            _ticks++;
            if (_ticks < TicksPerUpdate)
                return;
            _ticks -= TicksPerUpdate;

            switch (_state) {
                case State.Patrolling:
                    FindTarget();
                    if (patrollingUnit) {
                        if (_isPatrolling)
                            Patrol();
                    }

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
           // Debug.Log($"{ReachedPosition()} wayx:{ _waypoints[_x].position.x} player x: {transform.position.x}");
            return _waypoints[_x].position.z == transform.position.z &&
                   _waypoints[_x].position.x == transform.position.x;
        }

        void OnDrawGizmosSelected() {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, BasicEnemy.targetRange);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, BasicEnemy.stopChaseDistance);
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
            animator.SetTrigger("DroneR");
            BaseNavMeshAgent.SetDestination(StartingPosition);
            _isPatrolling = true;
            _state = State.Patrolling;
            FindTarget();
        }

        void ChaseTarget() {
            if (!_visibilityCheck.IsVisible(CombatTarget.gameObject, 200)) {
                LookAtTarget.enabled = false;
                BaseNavMeshAgent.isStopped = false;
                DeactivateAttack();
                _state = State.GoingBackToStart;
                return;
            }

            if(!patrollingUnit)BaseNavMeshAgent.SetDestination(CombatTarget.transform.position);
            if (Vector3.Distance(transform.position, CombatTarget.transform.position) < equipped.weapon.range) {
                BaseNavMeshAgent.isStopped = true;
                LookAtTarget.enabled = true;
                if (Vector3.Angle(transform.forward,
                    (CombatTarget.transform.position - transform.position).normalized) < 50)
                    EligibleToAttack = true;
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
            animator.SetTrigger("DroneR");

            // animator.ResetTrigger("DroneIdle");
            // animator.SetTrigger("DroneRunning");
            FindTarget();
        }


        void FindTarget() {
            if (Vector3.Distance(transform.position, CombatTarget.transform.position) < BasicEnemy.targetRange) {
                if (_visibilityCheck.IsVisible(CombatTarget.gameObject, BasicEnemy.targetRange)) {
                    _state = State.ChaseTarget;
                }
            }else DeactivateAttack();

            print("Eligible"+EligibleToAttack);
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