using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave360Anim : StateMachineBehaviour
{
    private BossWaves _bossWaves;
    private Vector3 _startScale;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves = animator.GetComponent<BossWaves>();
        _startScale = _bossWaves.wave360Transform.localScale;
        _bossWaves.wave360Transform.gameObject.SetActive(true);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _bossWaves.ScaleWave(true);
    }
}
