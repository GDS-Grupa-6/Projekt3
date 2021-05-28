using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float lifeTime = 10f;
    [SerializeField] private float damgeValue = 1f;

    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(rb.velocity);

        StartCoroutine(DestroyCorutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            Destroy(this.gameObject);
        }

        //dodać logikę zadawania obrażeń
    }

    IEnumerator DestroyCorutine()
    {
        yield return new WaitForSeconds(lifeTime);
        Destroy(this.gameObject);
    }
}
