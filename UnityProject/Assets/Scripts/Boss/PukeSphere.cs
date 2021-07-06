using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeSphere : MonoBehaviour
{
    [SerializeField] private float _fogPower = 1f;
    [SerializeField] private float _damageTakeDelay = 1f;

    private bool _playerIsSafe;
    private bool _corutineActive;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(TakeFogDamageCourutine(other.GetComponent<PlayerData>()));
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
            _corutineActive = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerIsSafe = true;
        }
    }

    private void Update()
    {
        if (!_playerIsSafe && !_corutineActive)
        {
            _corutineActive = true;
            StartCoroutine(TakeFogDamageCourutine(FindObjectOfType<PlayerData>()));
        }
    }

    private IEnumerator TakeFogDamageCourutine(PlayerData target)
    {
        while (true)
        {
            target.TakeDamage(_fogPower);
            yield return new WaitForSeconds(_damageTakeDelay);
        }
    }
}
