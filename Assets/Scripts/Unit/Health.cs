using System;
using GameStates;
using Player;
using UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Unit {
    [Serializable]
    public class FloatEvent : UnityEvent<float> {
    }

    [Serializable]
    public class BoolEvent : UnityEvent {
    }

    public class Health : MonoBehaviour, IResurrect {
        //[SerializeField] FloatEvent takingDamageEvent;
        //[SerializeField] BoolEvent deathEvent;
        [SerializeField] private UnitSfxIdEvent unitSfxIdEvent2D;
        [SerializeField] private UnitSfxIdEvent unitSfxIdEvent;
        int _currentCurrentHealth;
        
        //TODO: Update healthbar UI thru TakeDamage
        // public FloatEvent UpdateHealthUI;

        public int CurrentHealth {
            get => _currentCurrentHealth;
            set => _currentCurrentHealth =
                Mathf.Clamp(value, 0, GetComponent<IGetMaxHealth>().MaxHealth());
        }

        public int CalculateHealthBarValue() {
            var sliderValue = (float)CurrentHealth / GetComponent<IGetMaxHealth>().MaxHealth() * 100;
            return Mathf.RoundToInt(sliderValue);
        }
        
        public bool IsDead => CurrentHealth <= 0;

        public void TakeDamage(int damage) {
            //CurrentHealth -= damage;
            UnitSfxId id;
            if (IsDead && SceneManager.sceneCount == 1) {
                if(gameObject.layer == LayerMask.NameToLayer("Player"))
                    unitSfxIdEvent2D?.Invoke(id = UnitSfxId.Death);//Player
                else
                    unitSfxIdEvent?.Invoke(id = UnitSfxId.Death);//Enemies
            }
            else{
                if(CurrentHealth > 1 )
                    unitSfxIdEvent?.Invoke(id = UnitSfxId.TakingDamage);
                else{
                    if(gameObject.layer == LayerMask.NameToLayer("Player"))
                        unitSfxIdEvent2D?.Invoke(id = UnitSfxId.NearDeath);//Player
                    else
                        unitSfxIdEvent?.Invoke(id = UnitSfxId.TakingDamage);//Enemies
                }
            }
//            print(CurrentHealth);
        }

        public void OnResurrect(bool onCorpse) {
            //takingDamageEvent?.Invoke(CurrentHealth);
            GetComponent<Collider>().enabled = true;
        }
    }
}