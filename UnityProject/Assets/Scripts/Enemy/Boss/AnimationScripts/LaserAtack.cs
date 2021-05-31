using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserAtack : StateMachineBehaviour
{
    BossCombatLaser combatLaser;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatLaser = animator.GetComponent<BossCombatLaser>();  
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatLaser.CreateLaser();
        combatLaser.SpinBoss();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
