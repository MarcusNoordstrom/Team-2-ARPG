using System;
using Player;
using UnityEngine;
using Action = Player.Action;
using Random = UnityEngine.Random;

namespace Unit {
    [RequireComponent(typeof(VisibilityCheck))]
    public class StationaryEnemy : BaseUnit {
        [Header("Stationary Enemy Specific")] [SerializeField]
        Transform pivot;

        LookAtTarget _lookAtTarget;
        VisibilityCheck _visibilityCheck;
        int _ticks;
        const int TicksPerUpdate = 15;

        BasicEnemy _basicEnemy => (BasicEnemy) basicUnit; 
        protected override void Setup() {
            _lookAtTarget = GetComponent<LookAtTarget>();
            BaseHealth.CurrentHealth = basicUnit.maxHealth;
            equipped.ChangeWeapon(basicUnit.rangedWeapon);
            _visibilityCheck = GetComponent<VisibilityCheck>();

            _ticks = Random.Range(0, TicksPerUpdate);
        }

        void Start() {
            CombatTarget = FindObjectOfType<PlayerController>().gameObject;
            _lookAtTarget.Setup(CombatTarget.transform);
        }

        void FixedUpdate() {
            _ticks++;
            if (_ticks < TicksPerUpdate)
                return;
            _ticks -= TicksPerUpdate;

            if (_visibilityCheck.IsVisible(CombatTarget.gameObject, _basicEnemy.targetRange)) {
                _lookAtTarget.enabled = true;
                EligibleToAttack = true;

                if (Vector3.Angle(pivot.forward, (CombatTarget.transform.position - pivot.position).normalized) < 50)
                    return;
            }
            else {
                _lookAtTarget.enabled = false;
                DeactivateAttack();
            }
        }
        

    }
}