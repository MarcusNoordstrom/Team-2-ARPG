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
            if (_baseUnit.target == null || _baseUnit.target.GetComponent<Health>().IsDead) return;
            _baseUnit.baseEquippedWeapon.weapon.Attack(_baseUnit.bulletSpawnPoint.transform, _baseUnit.target);
            //TODO play muzzle effect when shooting
        }

        //animation event
        void RangedAttackFinishEvent() {
            if (_baseUnit.target == null || _baseUnit.target.GetComponent<Health>().IsDead) return;

            if (_baseUnit.target.layer == LayerMask.NameToLayer("Player")) {
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
            transform.LookAt(_baseUnit.target.transform);
            print(_baseUnit.target);
        }
    }
}