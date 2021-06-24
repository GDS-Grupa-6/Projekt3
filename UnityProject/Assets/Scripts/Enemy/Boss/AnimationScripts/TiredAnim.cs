using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiredAnim : StateMachineBehaviour
{
    private Puke _puke;
    private BossCombatLogic _bossCombatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _puke = animator.GetComponent<Puke>();
        _puke.megaPukeEnd = false;
        _puke.pukeEnd = false;
        animator.SetBool("MegaPukeEnd", _puke.megaPukeEnd);
        animator.SetBool("PukeEnd", _puke.pukeEnd);

        _bossCombatLogic = animator.GetComponent<BossCombatLogic>();
        _bossCombatLogic.CheckDistance();
    }
}
