using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//do zmiany??
public class LaserAtack : StateMachineBehaviour
{
    private BossCombatLaser _combatLaser;
    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _combatLaser = animator.GetComponent<BossCombatLaser>();
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _combatLaser.spinNumber = 0;
        _combatLaser.StartCoroutine(_combatLaser.ChangeLaserModeCourutine());
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (_combatLaser.spinNumber < _combatLaser.maxNumberOfSpin)
        {
            _combatLaser.CreateLaser();
            _combatLaser.SpinBoss();
        }
        else
        {
            _combatLaser.DestroyLaser();
            // zmiana stanu??
        }
    }
}
