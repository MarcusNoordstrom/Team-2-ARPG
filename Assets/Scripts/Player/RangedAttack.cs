using System;
using System.Collections;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class RangedAttack : MonoBehaviour, IAction {
        [SerializeField] string animationTrigger;
        [SerializeField] Transform bulletSpawnPointRotation;
        Animator _animator;
        BaseUnit _unit;
        SfxController sfxController => GetComponent<SfxController>();

        void Awake() {
            _unit = GetComponent<BaseUnit>();
            _animator = GetComponent<Animator>();
        }

        void Update() {
            if (PlayerHelper.UsingRangedAttack && GetComponent<PlayerController>() != null && !_unit.CombatTarget.GetComponent<Health>().IsDead) {
                if (!_unit.CanAttack || _unit.GetComponent<Health>().IsDead) return;
                _animator.ResetTrigger("Idle");
                _animator.ResetTrigger("Running");
                _animator.SetTrigger(animationTrigger);
                transform.LookAt(_unit.CombatTarget.transform);
                _unit._attackTimer = Time.time;
                ButtonCoolDown.rangeStartFilling = true;
                FindObjectOfType<ButtonCoolDown>().rangeAttackImage.fillAmount = 0;
            }
        }

        //animation event
        void RangedAttackEvent() {
            if (_unit.CombatTarget == null || _unit.CombatTarget.GetComponent<Health>().IsDead) return;

            _unit.equipped.weapon.Attack(_unit.bulletSpawnPoint.transform, _unit.CombatTarget);
            sfxController.OnPlay(UnitSfxId.Shoot);

            //TODO play muzzle effect when shooting
        }

        //animation event
        void RangedAttackFinishEvent() {
            if (_unit.CombatTarget == null || _unit.CombatTarget.GetComponent<Health>().IsDead) return;


            if (_unit.CombatTarget.layer == LayerMask.NameToLayer("Player")) {
                if (GetComponent<IAction>() != this) {
                    GetComponent<IAction>().ActionToStart();
                }
            }
        }


        public void ActionToStart() {
            _animator.ResetTrigger("Idle");
            if (GetComponent<NavMeshAgent>() != null) {
                GetComponent<NavMeshAgent>().isStopped = true;
            }

            if (GetComponent<PlayerController>() != null) {
                PlayerHelper.UsingRangedAttack = true;
                _unit._attackTimer = Time.time - _unit._attackTimer / 2;
            }
            else {
                _animator.SetTrigger(animationTrigger);
            }


            transform.LookAt(_unit.CombatTarget.transform);

            var targetPoint = _unit.CombatTarget.transform.position;
            targetPoint.x = _unit.CombatTarget.transform.position.x;
            targetPoint.y = _unit.CombatTarget.transform.position.y + 1;
            targetPoint.z = _unit.CombatTarget.transform.position.z;

            bulletSpawnPointRotation.transform.LookAt(targetPoint);
        }
    }
}