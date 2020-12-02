using UnityEngine;
using UnityEngine.UI;

namespace Unit{
    public class Health : MonoBehaviour{
        private int maxHealth = 100;//ScriptableObject: setup method or awake?
        private int health;
        public bool isDead;//Change to an event that the units component uses!
        public Slider healthSlider;
        
        public void TakeDamage(int damage){ 
            health = Mathf.Max(0, health - damage);
            if (health == 0){
                isDead = true;
            }
            healthSlider.value = health;
        }
        void Awake(){
            health = maxHealth;
            //TODO: 
        }
    }
}

