using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class MeleeAttack : MonoBehaviour, IAction {
        BaseUnit attackSource;

        EquippedWeapon EquippedWeapon => GetComponent<EquippedWeapon>();
        //print($"{PlayerController.PlayerTarget} {_navMeshAgent.isStopped}");

        NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();

        void Awake() {
            attackSource = GetComponent<BaseUnit>();
        }

        void Update() {

            if (attackSource.target == null) return;
            GetComponent<Action>().StartAction(this);
            MoveToTargetPosition();
            if (IsInMeleeRange()) {
                _navMeshAgent.isStopped = true;
                var targetPoint = attackSource.target.transform.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
                //TODO start attacking with melee weapon
            }
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(attackSource.target.transform.position);
        }

        public void ActionToStart() {
            _navMeshAgent.isStopped = false;
        }

        bool IsInMeleeRange() {
            return Vector3.Distance(transform.position, attackSource.target.transform.position) < attackSource.baseEquippedWeapon.weapon.range;
        }
    }
}