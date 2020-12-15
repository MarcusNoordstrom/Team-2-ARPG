﻿using System.Collections;
using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health, IResurrect {
    [SerializeField] Image lowHealthUI;
    [SerializeField] float alphaFadeSpeed = 0.1f;
    UnitSfxId id;
    bool _isFlashing;
    Color _fadeColor = new Color(255, 255, 255, 0);

    bool LowHealth => CurrentHealth == MaxHealth / 5;
    
    protected override void OnPlaySound() {
        if (LowHealth) {
            SfxController.OnPlay2D(id = UnitSfxId.NearDeath);
        }
        else {
            SfxController.OnPlay(id = UnitSfxId.TakingDamage);    
        }
        if (!IsDead) return;
        SfxController.OnPlay2D(id = UnitSfxId.Death);
    }

    void Flashing() {
        if (_isFlashing)
            return;
        if (lowHealthUI != null) {
            _isFlashing = true;
            StartCoroutine(ToggleState());
        }
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        if (!LowHealth) return;
        Flashing();
        _isFlashing = !IsDead;
        if (IsDead) {
            StopCoroutine(ToggleState());
        }
    }

    IEnumerator ToggleState() {
        while (true) {
            _fadeColor.a = Mathf.PingPong(Time.time * alphaFadeSpeed, 1);
            lowHealthUI.color = _fadeColor;
            yield return new WaitForFixedUpdate();
        }
    }
    
    public void OnResurrect(bool onCorpse) {
        SfxController.OnPlay2D(id = UnitSfxId.Resurrect);
        healthBar.value = MaxHealth;
        GetComponent<Collider>().enabled = true;
    }
}