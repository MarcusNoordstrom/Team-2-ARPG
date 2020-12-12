using System;
using Unit;
using UnityEngine;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        //TODO rotate player towards enemy
        Transform _target;

        void Update() {
            if (Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) {
                if (hit.collider.GetComponent<StationaryEnemy>() != null || (Input.GetKey(KeyCode.LeftShift) && Input.GetMouseButton(0))) {
                    _target = hit.collider.transform;
                    GetComponent<Action>().StartAction(this);
                }
            }
        }

        //animation event
        void Shoot() {
            //TODO create separate bullets for play, bullet script needs updating? Otherwise might need to create a new script for player specific bullets 
            foreach (var component in GetComponents<Attack>()) {
                if (component.weapon is IRange range) {
                    Instantiate(range.BulletPrefab(), transform.position, Quaternion.identity);
                }
            }
        }

        //animation event
        void ShootFinish() {
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        public void ActionToStart() {
            transform.LookAt(_target);
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }
    }
}