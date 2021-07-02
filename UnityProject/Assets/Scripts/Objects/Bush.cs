using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bush : MonoBehaviour
{
    [SerializeField] private bool _schowHud;
    [SerializeField] private Transform _hudPos;
    [SerializeField] private Slider _healthBar;
    [SerializeField] private bool _canHeal;
    [SerializeField] private float _healTime = 3;
    [SerializeField] private float _maxHealth;

    private float _currentHealth;
    private bool _healing;

    void Awake()
    {
        _currentHealth = _maxHealth;
        _healthBar.maxValue = _maxHealth;
        _healthBar.value = _currentHealth;

        if (!_schowHud)
        {
            _healthBar.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (_schowHud)
        {
            _healthBar.gameObject.transform.position = Camera.main.WorldToScreenPoint(_hudPos.position);
        }
    }

    public void TakeDamage(float value)
    {
        if (_currentHealth - value > 0)
        {
            _currentHealth -= value;
            _healthBar.value = _currentHealth;

            if (_canHeal && !_healing)
            {
                _healing = true;
                StartCoroutine(HealCourutine());
            }
        }
        else
        {
            _currentHealth = 0;
            Destroy(this.gameObject);
        }
    }

    private IEnumerator HealCourutine()
    {
        yield return new WaitForSeconds(_healTime);
        _currentHealth = _maxHealth;
        _healthBar.value = _currentHealth;
        _healing = false;
    }
}
