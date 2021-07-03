using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStartNextAttackSequenceAnim : StateMachineBehaviour
{
    private InputManager _inputManager;
    private MeleLogic _meleLogic;
    private bool _changeState;
    private Dash _dash;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _meleLogic = animator.GetComponent<MeleLogic>();
        _dash = animator.GetComponent<Dash>();
        _inputManager = _meleLogic.inputManager;
        _changeState = true;
        animator.SetBool("MeleInput", false);
        _dash.canDash = true;
        _meleLogic.cameraSwitch.canSwitchCameraState = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(1);
        AnimatorClipInfo[] animatorClipInfo = animator.GetCurrentAnimatorClipInfo(1);
        float time = animatorClipInfo[0].clip.length * animatorStateInfo.normalizedTime;

        if (_inputManager.PlayerAttacked() && _changeState && time >= animatorClipInfo[0].clip.length / 2f)
        {
            _meleLogic.cameraSwitch.canSwitchCameraState = false;
            _dash.canDash = false;
            _changeState = false;
            animator.SetBool("MeleInput", true);
            _meleLogic.MeleAttack();
            ChangeStateMele();
        }
    }

    private void ChangeStateMele()
    {
        if (_meleLogic.currentState.stateNumber + 1 < _meleLogic.meleStates.Length)
        {
            _meleLogic.currentState = _meleLogic.meleStates[_meleLogic.currentState.stateNumber + 1];
        }
    }
}
