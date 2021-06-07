using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private LayerMask ignoreLayerMask;
    [SerializeField] private Animator animator;

    public Transform doorL;
    public Transform doorR;
    public bool clicked;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer != ignoreLayerMask && !clicked)
        {
            animator.SetTrigger("Click");
            clicked = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer != ignoreLayerMask)
        {
            animator.SetTrigger("UnClick");
        }
    }
}
