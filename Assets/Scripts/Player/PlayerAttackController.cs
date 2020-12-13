using System;
using Unit;
using UnityEngine;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        //TODO rotate player towards enemy
        Vector3 _target;

        void Update() {
            if (Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) {
                if (hit.collider.GetComponent<StationaryEnemy>() != null || (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))) {
                    _target = hit.collider.transform.position;
                    GetComponent<Action>().StartAction(this);
                }
            }
        }

        //animation event
        void Shoot() {
            transform.LookAt(_target);
            //TODO create separate bullets for play, bullet script needs updating? Otherwise might need to create a new script for player specific bullets 
            GetComponent<Attack>().SpawnBullet();
        }

        //animation event
        void ShootFinish() {
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        public void ActionToStart() {
            transform.LookAt(new Vector3(_target.x, _target.y + 30, _target.z));
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }
    }
}