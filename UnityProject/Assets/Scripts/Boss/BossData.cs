using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum BossPhases { I, II, III }

[RequireComponent(typeof(Animator))]
public class BossData : MonoBehaviour
{
    [SerializeField] private Slider _healthSlider;
    [SerializeField] private TextMeshProUGUI _sliderText;
    [SerializeField] private float _phaseIHealth = 100f;
    [SerializeField] private float _phaseIIHealth = 110f;
    [SerializeField] private float _phaseIIIHealth = 120f;
    [SerializeField] private float _healSpeed = 0.2f;

    private BossPhases _bossPhase;
    private float _maxHealth;
    private float _currentHealth;
    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

        _bossPhase = BossPhases.I;
        _maxHealth = _phaseIHealth;
        _healthSlider.maxValue = _maxHealth;
        _currentHealth = _maxHealth;
        _healthSlider.value = _currentHealth;
        _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
    }

    public void TakeDamage(float value)
    {
        if (_currentHealth - value <= 0)
        {
            _currentHealth = 0;
            ChackPhase();
        }
        else
        {
            _currentHealth -= value;
            _healthSlider.value = _currentHealth;
            _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
        }
    }

    private void Dead()
    {
        _healthSlider.value = _currentHealth;
        _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
        _animator.SetTrigger("Dead");
        transform.eulerAngles = new Vector3(90, 0, 0); // tymczasowa logika
    }

    private void ChackPhase()
    {
        switch (_bossPhase)
        {
            case BossPhases.I:
                _bossPhase = BossPhases.II;
                _animator.SetTrigger("PhaseII");
                StartCoroutine(HealCourutine(_phaseIIHealth));
                break;
            case BossPhases.II:
                _bossPhase = BossPhases.III;
                _animator.SetTrigger("PhaseIII");
                StartCoroutine(HealCourutine(_phaseIIIHealth));
                break;
            case BossPhases.III:
                Dead();
                break;
            default:
                break;
        }
    }

    private IEnumerator HealCourutine(float maxHealthValue)
    {
        _maxHealth = maxHealthValue;
        _healthSlider.maxValue = _maxHealth;
        _healthSlider.value = _currentHealth;
        _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
        yield return new WaitForSeconds(_healSpeed);

        while (_currentHealth < maxHealthValue)
        {
            _currentHealth++;
            _healthSlider.value = _currentHealth;
            _sliderText.SetText($"{_currentHealth}/{_maxHealth}");
            yield return new WaitForSeconds(_healSpeed);
        }
    }
}
