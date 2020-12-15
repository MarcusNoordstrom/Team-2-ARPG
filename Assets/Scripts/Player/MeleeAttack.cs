﻿using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class MeleeAttack : MonoBehaviour, IAction {
        BaseUnit _baseUnit;

        //print($"{PlayerController.PlayerTarget} {_navMeshAgent.isStopped}");

        NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();

        void Awake() {
            _baseUnit = GetComponent<BaseUnit>();
        }

        void Update() {
            if (_baseUnit.CombatTarget == null) return;
            GetComponent<Action>().StartAction(this);
            MoveToTargetPosition();
            if (IsInMeleeRange()) {
                //TODO add cd on attack
                GetComponent<Animator>().SetTrigger("PlayerMeleeAttack");
                //TODO Trigger animation here
                _navMeshAgent.isStopped = true;
                var targetPoint = _baseUnit.CombatTarget.transform.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
                //TODO start attacking with melee weapon
            }
        }

        void Hit() {
            //TODO implement deal damage here
        }

        void MeleeAttackFinish() {
            //TODO repeat animation if player
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(_baseUnit.CombatTarget.transform.position);
        }

        public void ActionToStart() {
            _navMeshAgent.isStopped = false;
            
        }

        bool IsInMeleeRange() {
            return Vector3.Distance(transform.position, _baseUnit.CombatTarget.transform.position) < _baseUnit.baseEquippedWeapon.weapon.range;
        }
    }
}