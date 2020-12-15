using UnityEngine;
using UnityEngine.UI;

namespace Unit {
    [RequireComponent(typeof(SfxController))]
    public class Health : MonoBehaviour {
        protected SfxController SfxController => GetComponent<SfxController>();
        int _currentCurrentHealth;
        [SerializeField] protected Slider healthBar;
        protected Transform popupCanvas => GetComponentInChildren<Canvas>().transform;
        [SerializeField] DamageUI popupPrefab;
        protected int MaxHealth => GetComponent<IGetMaxHealth>().MaxHealth();

        void Start() {
            healthBar.maxValue = MaxHealth;
            healthBar.value = MaxHealth;
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.A)) {
                TakeDamage(20);
            }
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
            healthBar.value = CurrentHealth;
            OnPlaySound();
        }
    }
}