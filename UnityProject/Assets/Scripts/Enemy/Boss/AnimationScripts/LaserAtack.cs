using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAtack : StateMachineBehaviour
{
    BossCombatLaser combatLaser;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatLaser = animator.GetComponent<BossCombatLaser>();
        combatLaser.spinNumber = 0;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (combatLaser.spinNumber < combatLaser.maxNumberOfSpin)
        {
            combatLaser.CreateLaser();
            combatLaser.SpinBoss();
        }
        else
        {
            combatLaser.DestroyLaser();
            animator.SetTrigger("Normal");
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
