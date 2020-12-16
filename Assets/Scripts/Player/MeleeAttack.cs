﻿using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class MeleeAttack : MonoBehaviour, IAction {
        BaseUnit _baseUnit;

        //print($"{PlayerController.PlayerTarget} {_navMeshAgent.isStopped}");

        NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();
        private SfxController sfxController => GetComponent<SfxController>();

        bool _hasAttack;

        void Awake() {
            _baseUnit = GetComponent<BaseUnit>();
            
        }

        void Update() {
            if (_baseUnit.CombatTarget == null) return;
            MoveToTargetPosition();
            if (IsInMeleeRange() && _hasAttack && _baseUnit.CanAttack) {
                _hasAttack = false;
                GetComponent<Animator>().SetTrigger("PlayerMeleeAttack");
                _baseUnit._attackTimer = Time.time;
                _navMeshAgent.isStopped = true;
                var targetPoint = _baseUnit.CombatTarget.transform.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
            }
        }

        void Hit() {
            if(_baseUnit.CombatTarget == null) return;
            _baseUnit.equipped.weapon.Attack(transform, _baseUnit.CombatTarget);
            sfxController.OnPlay(UnitSfxId.Melee);
            //TODO implement deal damage here
        }

        void MeleeAttackFinish() {
            //TODO repeat animation if player
            _hasAttack = true;
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(_baseUnit.CombatTarget.transform.position);
        }

        public void ActionToStart() {
            _baseUnit._attackTimer = -_baseUnit.equipped.weapon.attackSpeed;
            _navMeshAgent.isStopped = false;
            _hasAttack = true;
        }

        bool IsInMeleeRange() {
            return Vector3.Distance(transform.position, _baseUnit.CombatTarget.transform.position) < _baseUnit.equipped.weapon.range;
        }
    }
}