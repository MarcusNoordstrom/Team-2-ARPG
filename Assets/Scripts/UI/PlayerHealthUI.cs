using System;
using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class PlayerHealthUI : MonoBehaviour, IResurrect {
        [SerializeField] GameObject healthBarUI;
        [SerializeField] DamageUI damageUIPrefab;
        [SerializeField] Transform popupCanvasParent;
        [SerializeField] float lowHealthTrigger;
        [SerializeField] BoolEvent lowHealthEvent;

        Health _health;
        bool _soundTriggered;

        void Start() {
            _health = GetComponent<Health>();
            healthBarUI.GetComponent<HealthBarUI>().InstantiateHealthTicks();
            Health.UpdatePlayerHealthUI += PlayerHealthStuff;
        }


        void PlayerHealthStuff(int damage) {
            UpdateHealthTicks(damage);
            //takingDamageEvent?.Invoke(CurrentHealth * 0.5f);
            if (LowHealth()) {
                _soundTriggered = true;
                lowHealthEvent?.Invoke();
            }

            SetupHealthBarUI();
        }


        void SetupHealthBarUI() {
            if (_health.CurrentHealth != GetComponent<IGetMaxHealth>().MaxHealth() && gameObject.layer == LayerMask.NameToLayer("Player")) {
                healthBarUI.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            }
        }

        bool LowHealth() {
            return _health.CurrentHealth <= lowHealthTrigger && !_soundTriggered;
        }

        void UpdateHealthTicks(int damage) {
            var damageUI = Instantiate(damageUIPrefab, popupCanvasParent.position, popupCanvasParent.rotation, popupCanvasParent);
            damageUI.SetUp(damage);
            healthBarUI.GetComponent<HealthBarUI>().RemoveHealthTick(damage);
        }

        public void OnResurrect(bool onCorpse) {
            _soundTriggered = false;
            healthBarUI.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            healthBarUI.GetComponent<HealthBarUI>().InstantiateHealthTicks();
        }
    }
}