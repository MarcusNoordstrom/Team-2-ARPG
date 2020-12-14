using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class MeleeAttackTemporary : MonoBehaviour {
        Attack _attack => GetComponent<Attack>();
        //print($"{PlayerController.PlayerTarget} {_navMeshAgent.isStopped}");

        NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();

        void Update() {
            
            if (Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) {
                if (hit.collider.GetComponent<StationaryEnemy>() != null && Input.GetKeyUp(KeyCode.Mouse1)) {
                    PlayerController.PlayerTarget = hit.collider.gameObject;
                }
            }

            if (!PlayerController.HasTarget) return;
            MoveToTargetPosition();
            if (InMeleeRange()) {
                _navMeshAgent.isStopped = true;
                var targetPoint = PlayerController.PlayerTarget.transform.position;
                targetPoint.y = transform.position.y;
                transform.LookAt(targetPoint);
                //TODO start attacking with melee weapon
            }
        }

        bool InMeleeRange() {
            return Vector3.Distance(transform.position, PlayerController.PlayerTarget.transform.position) < _attack.weapon.range;
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(PlayerController.PlayerTarget.transform.position);
        }
    }
}