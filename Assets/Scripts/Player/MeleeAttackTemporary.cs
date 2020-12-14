using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class MeleeAttackTemporary : MonoBehaviour {
        BaseUnit attackSource;

        Attack _attack => GetComponent<Attack>();
        //print($"{PlayerController.PlayerTarget} {_navMeshAgent.isStopped}");

        NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();

        void Awake() {
            this.attackSource = GetComponent<BaseUnit>();
        }

        void Update() {
            //TODO: Moveto PlayerController
            if (Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) {
                if (hit.collider.GetComponent<StationaryEnemy>() != null && Input.GetKeyUp(KeyCode.Mouse1)) {
                    PlayerController.PlayerTarget = hit.collider.gameObject;
                }
            }

            if (this.attackSource.target == null) return;
            MoveToTargetPosition();
            if (InMeleeRange()) {
                _navMeshAgent.isStopped = true;
                var targetPoint = attackSource.target.transform.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
                //TODO start attacking with melee weapon
            }
        }

        bool InMeleeRange() {
            return Vector3.Distance(transform.position, attackSource.target.transform.position) < _attack.weapon.range;
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(attackSource.target.transform.position);
        }
    }
}