using System;
using Player;
using Unit;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(VisibilityCheck))]
public class StationaryEnemy : BaseUnit {
    LookAtTarget _lookAtTarget;
    VisibilityCheck _visibilityCheck;
    int _ticks;
    const int TicksPerUpdate = 15;
    
    PlayerController _target => FindObjectOfType<PlayerController>();
    
    protected override void Setup() {
        _lookAtTarget = GetComponent<LookAtTarget>();
        BaseHealth.CurrentHealth = basicUnit.maxHealth;
        BaseAttack.ChangeWeapon(basicUnit.mainWeapon);
        _visibilityCheck = GetComponent<VisibilityCheck>();
        
        _ticks = Random.Range(0, TicksPerUpdate);
    }

    void Start() {
        _lookAtTarget.Setup(_target.transform);
    }

    void FixedUpdate() {
        _ticks++;
        if (_ticks < TicksPerUpdate)
            return;
        _ticks -= TicksPerUpdate;
        
        if (_visibilityCheck.IsVisible(_target.gameObject)) {
            
            _lookAtTarget.enabled = true;
            BaseAttack.ActivateAttack(_target.gameObject);
        }
        else {
            _lookAtTarget.enabled = false;
            BaseAttack.DeactivateAttack();
        }
    }
}