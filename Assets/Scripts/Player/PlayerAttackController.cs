﻿using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class PlayerAttackController : MonoBehaviour, IAction {
        
        GameObject _target;

        //TODO add a shift + left click to attack from current position towards where mouse is?
        void Update() {
            if (!Physics.Raycast(PlayerController.GetMouseRay(), out var hit)) return;
            if (hit.collider.GetComponent<StationaryEnemy>() == null) return;

            

            if (!Input.GetMouseButtonUp(0)) return;
            GetComponent<Animator>().SetTrigger("RangedAttack");
            GetComponent<Action>().StartAction(this);
            _target = hit.collider.gameObject;
            transform.LookAt(_target.transform.position);
        }

        //animation event
        void Shoot() {
            if(_target == null || _target.GetComponent<Health>().IsDead) return;
            GetComponent<Attack>().SpawnBullet();
            //TODO play muzzle effect when shooting
        }

        //animation event
        void ShootFinish() {
            if(_target == null || _target.GetComponent<Health>().IsDead) return;
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        //TODO check if in melee range when using melee attack
        bool InMeleeRange() {
            return false;
        }

        public void ActionToStart() {
            //_target = null;
        }
    }
}