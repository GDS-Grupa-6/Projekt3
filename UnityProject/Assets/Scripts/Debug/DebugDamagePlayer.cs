using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugDamagePlayer : MonoBehaviour
{
    [SerializeField] private bool rotateObj = true;
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] private float damageValue = 20f;

    PlayerData playerData;

    void Start()
    {
        playerData = FindObjectOfType<PlayerData>();    
    }

    void Update()
    {
        if (rotateObj)
        {
            transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerData.TakeDamage(damageValue);
        }
    }
}
