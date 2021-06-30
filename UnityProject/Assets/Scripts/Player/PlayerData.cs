using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Dash))]
public class PlayerData : MonoBehaviour
{
    [SerializeField] private float _maxHealth = 100;
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _sliderText;

    private float _currentHealth;
    private Dash _dash;

    private void Start()
    {
        _dash = GetComponent<Dash>();
        SetHealth();
    }

    public void TakeDamage(float damageValue)
    {
        if (!_dash.playerDashing)
        {
            if (_currentHealth - damageValue < 0)
            {
                _currentHealth = 0;
            }
            else
            {
                _currentHealth -= damageValue;
            }

            _healthSlider.value = _currentHealth;
            _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
        }
    }

    public void Heal(float healValue)
    {
        if (_currentHealth + healValue > _maxHealth)
        {
            _currentHealth = _maxHealth;
        }
        else
        {
            _currentHealth += healValue;
        }
    }

    private void SetHealth()
    {
        _currentHealth = _maxHealth;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _currentHealth;
        _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
    }
}
