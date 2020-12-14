using System;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class RangedAttack : MonoBehaviour, IAction {
        BaseUnit attackSource;
        BaseUnit _baseUnit;

        void Awake() {
            attackSource = GetComponent<BaseUnit>();
        }

        //animation event
        void Shoot() {
            if(attackSource.target == null || attackSource.target.GetComponent<Health>().IsDead) return;
            attackSource.baseEquippedWeapon.weapon.Attack(attackSource.bulletSpawnPoint.transform , attackSource.target);
            //TODO play muzzle effect when shooting
        }

        //animation event
        void ShootFinish() {
            if (attackSource.target == null || attackSource.target.GetComponent<Health>().IsDead) return;
            GetComponent<Animator>().SetTrigger("RangedAttack");
        }

        //TODO check if in melee range when using melee attack


        public void ActionToStart() {
            GetComponent<NavMeshAgent>().isStopped = true;
            GetComponent<Animator>().SetTrigger("RangedAttack");
            transform.LookAt(attackSource.target.transform.position);
        }
    }
}