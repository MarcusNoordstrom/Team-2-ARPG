using System;
using UnityEngine;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        
        //TODO rotate player towards enemy
        
        void Update() {
            if (Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) {
                if (hit.collider.GetComponent<StationaryEnemy>() != null) {
                    GetComponent<Action>().StartAction(this);
                }
            }
        }

        //animation event
        void Shoot() {
            //TODO instantiate bullets here
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