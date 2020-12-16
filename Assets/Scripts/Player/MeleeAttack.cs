using System;
using UI;
using Unit;
using UnityEngine;
using UnityEngine.AI;

namespace Player {
    public class MeleeAttack : MonoBehaviour, IAction {
        BaseUnit _baseUnit;

        NavMeshAgent _navMeshAgent => GetComponent<NavMeshAgent>();
        SfxController sfxController => GetComponent<SfxController>();

        bool _hasAttack;

        void Awake() {
            _baseUnit = GetComponent<BaseUnit>();
            _baseUnit._attackTimer = -_baseUnit.equipped.weapon.attackSpeed;
        }

        void Update() {
            if (_baseUnit.CombatTarget == null) return;
            MoveToTargetPosition();
            if (IsInMeleeRange()) {
                LookAtTarget();
                _navMeshAgent.isStopped = true;
                if (_hasAttack && _baseUnit.CanAttack) {
                    _hasAttack = false;
                    GetComponent<Animator>().SetTrigger("PlayerMeleeAttack");
                    _baseUnit._attackTimer = Time.time;
                    StartFillingCd();
                }
            }
        }

        void LookAtTarget() {
            var targetPoint = _baseUnit.CombatTarget.transform.position;
            targetPoint.y = transform.position.y;
            transform.LookAt(targetPoint);
        }

        void Hit() {
            if (_baseUnit.CombatTarget == null) return;
            _baseUnit.equipped.weapon.Attack(transform, _baseUnit.CombatTarget);
            sfxController.OnPlay(UnitSfxId.Melee);
        }

        void MeleeAttackFinish() {
            _hasAttack = true;

            if (_baseUnit.GetComponent<PlayerController>() != null) {
                ButtonCoolDown.meleeStartFilling = true;
            }
        }

        void MoveToTargetPosition() {
            _navMeshAgent.SetDestination(_baseUnit.CombatTarget.transform.position);
        }

        public void ActionToStart() {
            _hasAttack = true;
        }

        void StartFillingCd() {
            if (_baseUnit.GetComponent<PlayerController>() != null) {
                FindObjectOfType<ButtonCoolDown>().meleeAttackImage.fillAmount = 0;
                ButtonCoolDown.meleeStartFilling = true;
            }
        }

        bool IsInMeleeRange() {
            return Vector3.Distance(transform.position, _baseUnit.CombatTarget.transform.position) < _baseUnit.equipped.weapon.range;
        }
    }
}