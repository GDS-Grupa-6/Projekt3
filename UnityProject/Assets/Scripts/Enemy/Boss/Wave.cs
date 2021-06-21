using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [HideInInspector] public float power;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Gracz dosta³ z fali: " + this.gameObject.name);
        }
    }
}
