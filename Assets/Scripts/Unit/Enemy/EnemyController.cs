﻿using Player;
using UnityEngine;

namespace Unit {
    [RequireComponent(typeof(VisibilityCheck), typeof(LookAtTarget))]
    public class EnemyController : BaseUnit {
        const int TicksPerUpdate = 15;
        BasicEnemy BasicEnemy => (BasicEnemy)basicUnit;
        LookAtTarget LookAtTarget => GetComponent<LookAtTarget>();
        Vector3 _roamPosition;
        Vector3 StartingPosition => transform.position;
        State _state;
        PlayerController _target => FindObjectOfType<PlayerController>();
        int _ticks;
        VisibilityCheck _visibilityCheck;
        
        void Start() {
            _visibilityCheck = GetComponent<VisibilityCheck>();
            _state = State.Roaming;
            LookAtTarget.Setup(_target.transform);
            _roamPosition = GetRoamingPosition();
            _ticks = Random.Range(0, TicksPerUpdate);
        }

        void FixedUpdate() {
            _ticks++;
            if (_ticks < TicksPerUpdate)
                return;
            _ticks -= TicksPerUpdate;

            switch (_state) {
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
            Gizmos.DrawWireSphere(transform.position, BasicEnemy.targetRange);
        }

        void GoingBackToStart() {
            BaseNavMeshAgent.SetDestination(StartingPosition);
            if (Vector3.Distance(transform.position, StartingPosition) < 1f) {
                _state = State.Roaming;
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

        void RoamToNewPosition() {
            BaseNavMeshAgent.SetDestination(_roamPosition);
            if (Vector3.Distance(transform.position, _roamPosition) < 1f)
                _roamPosition = GetRoamingPosition();
            FindTarget();
        }

        static Vector3 GetRandomDir() {
            return new Vector3(Random.Range(-1f, 1f), 0, Random.Range(-1f, 1f)).normalized;
        }

        Vector3 GetRoamingPosition() {
            return StartingPosition + GetRandomDir() * Random.Range(3f, 10f);
        }

        void FindTarget() {
            if (Vector3.Distance(transform.position, _target.transform.position) < BasicEnemy.targetRange)
                if (_visibilityCheck.IsVisible(_target.gameObject)) {
                    _roamPosition = GetRoamingPosition();
                    _state = State.ChaseTarget;
                }
        }

        enum State {
            Roaming,
            ChaseTarget,
            GoingBackToStart
        }
    }
}