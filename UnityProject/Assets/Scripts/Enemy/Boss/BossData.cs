using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(Animator))]
public class BossData : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private float _maxHealth;
    private float _currentHealth;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _healthSlider.maxValue = _maxHealth;
        _currentHealth = _maxHealth;
        _healthSlider.value = _currentHealth;
        _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
    }

    public void TakeDamage(float value)
    {
        _currentHealth -= value;

        if (_currentHealth <= 0)
        {
            _currentHealth = 0;
            Dead();
        }

        _healthSlider.value = _currentHealth;
        _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
    }

    private void Dead()
    {
        _animator.SetTrigger("Dead");
        transform.eulerAngles = new Vector3(90, 0, 0); // tymczasowa logika
        Debug.Log("<color=orange>Boss kopn¹³ w kalendarz</color>");
    }
}
