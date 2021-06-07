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
        bossMovement.timeParabolaJump = 0;
        bossMovement.startBossPosition = animator.transform.position;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(animator.transform.position, bossMovement.bossTargetTransform.position) <= 0.5f)
        {
            animator.SetTrigger("Atack");
        }
        else
        {
            bossMovement.ParabolaJump();
        }
    }
}
