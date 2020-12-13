using System;
using Unit;
using UnityEngine;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        //TODO rotate player towards enemy
        Vector3 _target;

        void Update() {
            if (Physics.Raycast(PlayerController.GetMouseRay(), out var hit) ) {
                if (hit.collider.GetComponent<StationaryEnemy>() != null || (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))) {
                    _target = hit.collider.transform.position;
                    GetComponent<Action>().StartAction(this);
                    if (Input.GetMouseButtonUp(0)) {
                        transform.LookAt(_target);
                    }
                }
            }
        }

        //animation event
        void Shoot() {
            //transform.LookAt(_target);
            GetComponent<Attack>().SpawnBullet();
        }

        //animation event
        void ShootFinish() {
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        public void ActionToStart() {
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }
    }
}