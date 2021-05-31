using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserMove : StateMachineBehaviour
{
    BossCombatLaser combatLaser;
    BossMovement bossMovement;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        combatLaser = animator.GetComponent<BossCombatLaser>();
        bossMovement = animator.GetComponent<BossMovement>();
        combatLaser.SelectBossLaserPosition();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossMovement.Teleport();

        if (animator.transform.position == bossMovement.bossTargetPosition.position)
        {
            animator.SetTrigger("Atack");
        }
    }
}
