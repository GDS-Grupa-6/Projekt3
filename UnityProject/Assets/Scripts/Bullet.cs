using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Bullet : MonoBehaviour
{
    private Rigidbody rb;
    private float lifeTime = 2f;
    private float timer;
    private bool hitSomething;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        transform.rotation = Quaternion.LookRotation(rb.velocity);
    }

    private void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null)
        {
            rb.isKinematic = true;
            // Destroy(this.gameObject);
        }
    }
}
