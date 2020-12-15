using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Health, IResurrect {
    UnitSfxId id;
    bool _isFlashing;
    bool _soundTriggered;
    [SerializeField] float duration = 0.5f;
    [SerializeField] float interval = 1f;
    [SerializeField] Image lowHealthUI;
    [SerializeField] float lowHealthTrigger;
    [SerializeField] float alphaFadeSpeed = 1f;
    
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
            InvokeRepeating("ToggleState", duration, interval);
        }
    }

    public override void TakeDamage(int damage) {
        base.TakeDamage(damage);
        if (!LowHealth) return;
        lowHealthUI.color = new Color(255, 255, 255, alphaFadeSpeed += Time.deltaTime);
        Flashing();
        _isFlashing = !IsDead;
    }

    void ToggleState() {
        lowHealthUI.enabled = !lowHealthUI.enabled;
    }
    
    public void OnResurrect(bool onCorpse) {
        SfxController.OnPlay2D(id = UnitSfxId.Resurrect);
        healthBar.value = MaxHealth;
        GetComponent<Collider>().enabled = true;
    }
}