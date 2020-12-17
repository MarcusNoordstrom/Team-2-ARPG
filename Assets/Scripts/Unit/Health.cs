using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Unit {
    [RequireComponent(typeof(SfxController))]
    public class Health : MonoBehaviour {
        protected SfxController SfxController => GetComponent<SfxController>();
        int _currentCurrentHealth;
        [SerializeField] protected Slider healthBar;
        [SerializeField] Transform popupCanvas;
        [SerializeField] DamageUI popupPrefab;
        protected int MaxHealth => GetComponent<IGetMaxHealth>().MaxHealth();

        public UnityAction DeadStuff;

        void Start() {
            healthBar.maxValue = MaxHealth;
            healthBar.value = MaxHealth;
        }
        public int CurrentHealth {
            get => _currentCurrentHealth;
            set => _currentCurrentHealth =
                Mathf.Clamp(value, 0, MaxHealth);
        }

        public bool IsDead => CurrentHealth <= 0;

        protected virtual void OnPlaySound() {
            UnitSfxId id;
            SfxController.OnPlay(id = UnitSfxId.TakingDamage);
            if (!IsDead) return;
            SfxController.OnPlay(id = UnitSfxId.Death);
        }

        void SpawnPopup(int damage) {
            var popup = Instantiate(popupPrefab, popupCanvas);
            popup.SetUp(damage);
        }

        public virtual void TakeDamage(int damage) {
            SpawnPopup(damage);
            CurrentHealth -= damage;
            if (IsDead) DeadStuff();
            healthBar.value = CurrentHealth;
            OnPlaySound();
        }
    }
}