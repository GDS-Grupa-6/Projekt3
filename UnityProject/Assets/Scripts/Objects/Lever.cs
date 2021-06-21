using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody))] //freez object!
public class Lever : MonoBehaviour
{
    [SerializeField] private Animator _doorAnimator;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"
            /* || other.gameObject.tag == "ewentualny inny obiekt" */)
        {
            _animator.SetTrigger("Click");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player"
            /* || other.gameObject.tag == "ewentualny inny obiekt" */)
        {
            _animator.SetTrigger("UnClick");
        }
    }
}
