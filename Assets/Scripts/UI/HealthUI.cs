using System;
using GameStates;
using Unit;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class HealthUI : MonoBehaviour, IResurrect {
        [SerializeField] GameObject healthBarUI;
        [SerializeField] DamageUI damageUIPrefab;
        [SerializeField] Transform damageUISpawnLocation;
        [SerializeField] float lowHealthTrigger;
        [SerializeField] BoolEvent lowHealthEvent;

        Transform _popupCanvasParent;
        Health _health;
        bool _soundTriggered;

        void Start() {
            _popupCanvasParent = FindObjectOfType<CanvasFollowCamera>().transform;
            _health = GetComponent<Health>();
            Health.CurrentHealthBars = _health.CurrentHealth;

            //healthBarUI.GetComponent<HealthBarUI>().InstantiateHealthTicks();
            _health.UpdatePlayerHealthUI += PlayerHealthStuff;
        }


        void PlayerHealthStuff(int damage) {
            if (LayerMask.GetMask() == LayerMask.NameToLayer("Player")) return;

            UpdateHealthTicks(damage);
            //takingDamageEvent?.Invoke(CurrentHealth * 0.5f);
            if (LowHealth()) {
                _soundTriggered = true;
                lowHealthEvent?.Invoke();
            }

            //SetupHealthBarUI();
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
            var damageUI = Instantiate(damageUIPrefab, damageUISpawnLocation.position, damageUISpawnLocation.rotation, damageUISpawnLocation);
            damageUI.SetUp(damage);
            //healthBarUI.GetComponent<HealthBarUI>().RemoveHealthTick(damage);
        }

        public void OnResurrect(bool onCorpse) {
            _soundTriggered = false;
            healthBarUI.GetComponent<HorizontalLayoutGroup>().childControlWidth = false;
            healthBarUI.GetComponent<HealthBarUI>().InstantiateHealthTicks();
        }
    }
}