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
        [SerializeField] float alphaFadeSpeed = 1f;
        
        public Image lowHealthUI;
        public float interval = 1f;
        public float duration = 0.5f;
        private bool isFlashing = false;

        Health _health;
        bool _soundTriggered;

        void Start() {
            _health = GetComponent<Health>();
            _health.UpdatePlayerHealthUI += PlayerHealthStuff;
        }
        

        void PlayerHealthStuff(int damage) {
            if (LayerMask.GetMask() == LayerMask.NameToLayer("Player")) return;
            
            var sliderValue = (float)_health.CurrentHealth / GetComponent<IGetMaxHealth>().MaxHealth() * 100;
            healthBarUI.GetComponent<Slider>().value = Mathf.RoundToInt(sliderValue);
            
            // takingDamageEvent?.Invoke(CurrentHealth * 0.5f);
            if (LowHealth()) {
                _soundTriggered = true;
                lowHealthEvent?.Invoke();
                lowHealthUI.color = new Color(255, 255, 255, alphaFadeSpeed += Time.deltaTime);
                Flashing();
                Debug.Log("Color");
            }

            //SetupHealthBarUI();
        }

        bool LowHealth() {
            return _health.CurrentHealth <= lowHealthTrigger && !_soundTriggered;
        }

        public void OnResurrect(bool onCorpse) {
            _soundTriggered = false;
            healthBarUI.GetComponent<Slider>().value = _health.CurrentHealth * 0.1f;
        }

        public void Flashing() {
            if (isFlashing)
                return;
            if (lowHealthUI != null) {
                isFlashing = true;
                InvokeRepeating("ToggleState", duration, interval);
            }
        }

        public void ToggleState() {
            lowHealthUI.enabled = !lowHealthUI.enabled;
        }
    }
}