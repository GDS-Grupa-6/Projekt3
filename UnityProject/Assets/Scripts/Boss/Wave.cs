using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [SerializeField] private float power;

    private void Awake()
    {
        DesactiveWave();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerData>().TakeDamage(power);
        }
    }

    public void ActiveWave()
    {
        this.gameObject.SetActive(true);
    }

    public void DesactiveWave()
    {
        this.gameObject.SetActive(false);
    }
}
