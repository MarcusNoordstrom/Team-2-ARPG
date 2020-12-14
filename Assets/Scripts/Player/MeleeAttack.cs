using System;
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
            if (_baseUnit.target == null) return;
            GetComponent<Action>().StartAction(this);
            MoveToTargetPosition();
            if (IsInMeleeRange()) {
                _navMeshAgent.isStopped = true;
                var targetPoint = _baseUnit.target.transform.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
                //TODO start attacking with melee weapon
            }
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(_baseUnit.target.transform.position);
        }

        public void ActionToStart() {
            _navMeshAgent.isStopped = false;
        }

        bool IsInMeleeRange() {
            return Vector3.Distance(transform.position, _baseUnit.target.transform.position) < _baseUnit.baseEquippedWeapon.weapon.range;
        }
    }
}