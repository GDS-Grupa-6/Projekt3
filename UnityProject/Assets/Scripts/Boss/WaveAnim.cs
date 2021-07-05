using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveAnim : StateMachineBehaviour
{
    [SerializeField] private bool _wave360;

    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _combatLogic = animator.GetComponent<BossCombatLogic>();

        if (_wave360)
        {
            _combatLogic.wave360.ActiveWave();
        }
        else
        {
            _combatLogic.wave45.ActiveWave();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_wave360)
        {
            _combatLogic.wave360.DesactiveWave();
        }
        else
        {
            _combatLogic.wave45.DesactiveWave();
            _combatLogic.SetCombatState(BossCobatStates.Locked);
        }
    }
}
