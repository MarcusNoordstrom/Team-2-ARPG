using UnityEngine;
using UnityEngine.UI;

namespace Unit{
    public class Health : MonoBehaviour{
        [SerializeField]private Slider healthSlider;
        [SerializeField]private GameObject damageUIPrefab;
        [SerializeField]private Transform parent;
        private int _maxHealth = 100;//ScriptableObject: setup method or awake?
        private int _health;
        private bool _isDead;//Change to an event that the units component uses!
        public bool IsDead => _isDead;
        
        public void TakeDamage(int damage){ 
            _health = Mathf.Max(0, _health - damage);
            var damageUI = Instantiate(damageUIPrefab, parent.position,
                parent.rotation, parent);
            damageUI.GetComponent<DamageUI>().SetUp(damage);
            if (_health == 0){
                _isDead = true;
            }
            healthSlider.value = _health;
        }
        private void Awake(){
            _health = _maxHealth;
        }
    }
}

