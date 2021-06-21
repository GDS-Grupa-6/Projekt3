using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))] //freez object!
public class Lever : MonoBehaviour
{
    [SerializeField] private Animator doorAnimator;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"
            /* || other.gameObject.tag == "ewentualny inny obiekt" */)
        {
            animator.SetTrigger("Click");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"
            /* || other.gameObject.tag == "ewentualny inny obiekt" */)
        {
            animator.SetTrigger("UnClick");
        }
    }
}
