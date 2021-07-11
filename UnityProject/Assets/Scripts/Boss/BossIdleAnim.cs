using UnityEngine;

public class BossIdleAnim : StateMachineBehaviour
{
    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _combatLogic = animator.GetComponent<BossCombatLogic>();

        animator.SetInteger("MoveTime", _combatLogic.changeAttackWhenFollowTime);

        if (_combatLogic.changeAttackFromMove)
        {
            _combatLogic.DistanceAttackStates();
            _combatLogic.changeAttackFromMove = false;
        }
        else
        {
            _combatLogic.CheckDistanceForStates();
        }
    }
}
