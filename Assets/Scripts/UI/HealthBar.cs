using Unit;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour {
    public Slider healthbar;
    IGetMaxHealth _maxHealth => GetComponent<IGetMaxHealth>();

    void Start() {
        healthbar.maxValue = _maxHealth.MaxHealth();
        healthbar.value = _maxHealth.MaxHealth();
    }

    public void UpdateHealthbar(int damage) {
        healthbar.value -= damage;
    }
}