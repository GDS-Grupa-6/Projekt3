using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaPukeAnim : StateMachineBehaviour
{
    private Puke _puke;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _puke = animator.GetComponent<Puke>();
        _puke.StartMegaPuke();
    }


    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("MegaPukeEnd", _puke.megaPukeEnd);
    }
}
