using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanStartNextAttackSequenceAnim : StateMachineBehaviour
{
    private InputManager _inputManager;
    private MeleLogic _meleLogic;
    private bool _changeState;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _meleLogic = animator.GetComponent<MeleLogic>();
        _inputManager = _meleLogic.inputManager;
        _changeState = true;
        animator.SetBool("MeleInput", false);
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_inputManager.PlayerAttacked() && _changeState)
        {
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
