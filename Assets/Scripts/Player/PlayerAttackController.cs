using System;
using Unit;
using UnityEngine;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        GameObject _target;

        void Update() {
            if (!Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) return;
            if (hit.collider.GetComponent<StationaryEnemy>() == null) return;

            GetComponent<Action>().StartAction(this);

            if (!Input.GetMouseButtonUp(0)) return;
            _target = hit.collider.gameObject;
            transform.LookAt(_target.transform.position);
        }

        //animation event
        void Shoot() {
            GetComponent<Attack>().SpawnBullet();
        }

        //animation event
        void ShootFinish() {
            if(_target.GetComponent<Health>().IsDead) return;
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        public void ActionToStart() {
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }
    }
}