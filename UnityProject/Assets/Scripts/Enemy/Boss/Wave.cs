using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [HideInInspector] public float power;

    private PlayerData _playerData;

    private void Awake()
    {
        _playerData = FindObjectOfType<PlayerData>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _playerData.TakeDamage(power);
        }
    }
}
