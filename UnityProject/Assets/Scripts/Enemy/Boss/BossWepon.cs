using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWepon : MonoBehaviour
{
    [HideInInspector] public float power = 10;

    private PlayerData _playerData;
    private Collider _collider;

    private void Awake()
    {
        _playerData = FindObjectOfType<PlayerData>();
        _collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerData.TakeDamage(power);
        }
    }
}
