using System;
using System.Collections;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class RangedAttack : MonoBehaviour, IAction {
        [SerializeField] string animationTrigger;
        Animator _animator;
        BaseUnit _unit;
        private SfxController sfxController => GetComponent<SfxController>();
        void Awake() {
            _unit = GetComponent<BaseUnit>();
            _animator = GetComponent<Animator>();
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
                     return;
                 }
             }

            GetComponent<Animator>().SetTrigger(animationTrigger);
            transform.LookAt(_unit.CombatTarget.transform);
        }
        
        public void ActionToStart() {
            _animator.ResetTrigger("Idle");
            if (GetComponent<NavMeshAgent>() != null) {
                GetComponent<NavMeshAgent>().isStopped = true;
            }

            _animator.SetTrigger(animationTrigger);
            transform.LookAt(_unit.CombatTarget.transform);
        }
    }
}