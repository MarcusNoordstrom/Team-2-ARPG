﻿using System;
using System.Collections;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class RangedAttack : MonoBehaviour, IAction {
        [SerializeField] GameObject muzzleEffectSpawnLocation;
        [SerializeField] GameObject muzzleEffect;
        [SerializeField] string animationTrigger;
        [SerializeField] Transform bulletSpawnPointRotation;
        Animator _animator;
        BaseUnit _unit;
        SfxController sfxController => GetComponent<SfxController>();

        void Awake() {
            _unit = GetComponent<BaseUnit>();
            _animator = GetComponent<Animator>();
        }

        //animation event
        void RangedAttackEvent() {
            if (_unit.CombatTarget == null || _unit.CombatTarget.GetComponent<Health>().IsDead) return;

            _unit.equipped.weapon.Attack(_unit.bulletSpawnPoint.transform, _unit.CombatTarget);
            sfxController.OnPlay(UnitSfxId.Shoot);

            if (muzzleEffect != null || muzzleEffectSpawnLocation != null) {
                var effect = Instantiate(muzzleEffect, muzzleEffectSpawnLocation.transform.position, transform.rotation);
                Destroy(effect, 1f);
            }
        }

        //animation event
        void RangedAttackFinishEvent() {
            if (_unit.CombatTarget == null || _unit.CombatTarget.GetComponent<Health>().IsDead) return;
            if (_unit.CombatTarget == GetComponent<PlayerController>().gameObject) return;

            if (_unit.CombatTarget.layer != LayerMask.NameToLayer("Player")) {
                if (GetComponent<IAction>() != this) {
                    GetComponent<IAction>().ActionToStart();
                }
            }

            _animator.SetTrigger(animationTrigger);
            transform.LookAt(_unit.CombatTarget.transform);

            var targetPoint = _unit.CombatTarget.transform.position;
            targetPoint.x = _unit.CombatTarget.transform.position.x;
            targetPoint.y = _unit.CombatTarget.transform.position.y + 1;
            targetPoint.z = _unit.CombatTarget.transform.position.z;

            bulletSpawnPointRotation.transform.LookAt(targetPoint);
        }


        public void ActionToStart() {
            _animator.ResetTrigger("Idle");
            if (GetComponent<NavMeshAgent>() != null) {
                GetComponent<NavMeshAgent>().isStopped = true;
            }

            if (GetComponent<PlayerController>() != null) {
                PlayerHelper.UsingRangedAttack = true;
            }
            _animator.SetTrigger(animationTrigger);


            transform.LookAt(_unit.CombatTarget.transform);

            var targetPoint = _unit.CombatTarget.transform.position;
            targetPoint.x = _unit.CombatTarget.transform.position.x;
            targetPoint.y = _unit.CombatTarget.transform.position.y + 1;
            targetPoint.z = _unit.CombatTarget.transform.position.z;

            bulletSpawnPointRotation.transform.LookAt(targetPoint);
        }
    }
}