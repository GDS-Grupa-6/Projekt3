using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave360Anim : StateMachineBehaviour
{
    private BossWaves _bossWaves;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves = animator.GetComponent<BossWaves>();
        _bossWaves.ActiveWave(true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves.ScaleWave(true);
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves.DesactiveWave(true);
    }
}
