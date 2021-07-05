using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeSphere : MonoBehaviour
{
    [SerializeField] private float _fogPower = 1f;
    [SerializeField] private float _damageTakeDelay = 1f;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(TakeFogDamageCourutine(other.gameObject));
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator TakeFogDamageCourutine(GameObject target)
    {
        while (true)
        {
            target.GetComponent<PlayerData>().TakeDamage(_fogPower);
            yield return new WaitForSeconds(_damageTakeDelay);
        }
    }
}
