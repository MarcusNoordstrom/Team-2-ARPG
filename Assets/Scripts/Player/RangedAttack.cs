using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class RangedAttack : MonoBehaviour, IAction {
        [SerializeField] string animationTrigger;
        BaseUnit _baseUnit;


        void Awake() {
            _baseUnit = GetComponent<BaseUnit>();
        }

        //animation event
        void RangedAttackEvent() {
            if (_baseUnit.CombatTarget == null || _baseUnit.CombatTarget.GetComponent<Health>().IsDead) return;
            GetComponent<NavMeshAgent>().isStopped = true;
            _baseUnit.baseEquippedWeapon.weapon.Attack(_baseUnit.bulletSpawnPoint.transform, _baseUnit.CombatTarget);
            //TODO play muzzle effect when shooting
        }

        //animation event
        void RangedAttackFinishEvent() {
            if (_baseUnit.CombatTarget == null || _baseUnit.CombatTarget.GetComponent<Health>().IsDead) return;

            if (_baseUnit.CombatTarget.layer == LayerMask.NameToLayer("Player")) {
                if (GetComponent<IAction>() != this) {
                    GetComponent<IAction>().ActionToStart();
                    return;
                }
            }

            
            GetComponent<Animator>().SetTrigger(animationTrigger);
        }

        //TODO check if in melee range when using melee attack


        public void ActionToStart() {
            if (GetComponent<NavMeshAgent>() != null) {
                GetComponent<NavMeshAgent>().isStopped = true;
            }

            GetComponent<Animator>().SetTrigger(animationTrigger);

            transform.LookAt(_baseUnit.CombatTarget.transform);
            print(_baseUnit.CombatTarget);
        }
    }
}