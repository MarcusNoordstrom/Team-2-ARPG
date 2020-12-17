using System;
using System.Collections;
using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI {
    [Serializable] public class VoidEvent : UnityEvent{}
    public class PlayerHealth : Health, IResurrect {
        [SerializeField] Image lowHealthUI;
        [SerializeField] float alphaFadeSpeed = 0.1f;
        UnitSfxId id;
        bool _isFlashing;
        Color _fadeColor = new Color(255, 255, 255, 0);
        bool LowHealth => CurrentHealth == MaxHealth / 5;
        [SerializeField] VoidEvent deathEvent;
        [SerializeField] VoidEvent resurrectEvent;
        
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
            if (IsDead) {
                deathEvent?.Invoke(); 
                StopAllCoroutines();
                _fadeColor.a = 0;
                lowHealthUI.color = _fadeColor;
            }
            if (!LowHealth) return;
            Flashing();
            _isFlashing = !IsDead;
        }

        IEnumerator ToggleState() {
            while (true) {
                _fadeColor.a = Mathf.PingPong(Time.time * alphaFadeSpeed, 1);
                lowHealthUI.color = _fadeColor;
                yield return new WaitForFixedUpdate();
            }
        }
    
        public void OnResurrect(bool onCorpse) {
            resurrectEvent?.Invoke();
            SfxController.OnPlay2D(id = UnitSfxId.Resurrect);
            healthBar.value = MaxHealth;
            GetComponent<Collider>().enabled = true;
        }
    }
}