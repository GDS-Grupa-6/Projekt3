using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PukeAnim : StateMachineBehaviour
{
    private Puke _puke;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _puke = animator.GetComponent<Puke>();
        _puke.StartPuke();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("PukeEnd", _puke.pukeEnd);
    }
}
