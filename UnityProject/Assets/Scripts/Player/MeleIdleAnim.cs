using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleIdleAnim : StateMachineBehaviour
{
    private MeleLogic _meleLogic;
    private PlayerData _playerData;
    private Dash _dash;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _dash = animator.GetComponent<Dash>();
        _meleLogic = animator.GetComponent<MeleLogic>();
        _playerData = animator.GetComponent<PlayerData>();
        _playerData.hitTaken = false;
        _dash.canDash = true;
        _meleLogic.cameraSwitch.canSwitchCameraState = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _meleLogic.canStartNextSequence = true;
        _meleLogic.currentState = _meleLogic.meleStates[0];
        animator.SetBool("MeleInput", false);
    }
}
