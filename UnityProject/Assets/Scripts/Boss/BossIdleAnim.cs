using UnityEngine;

public class BossIdleAnim : StateMachineBehaviour
{
    private BossCombatLogic _combatLogic;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _combatLogic = animator.GetComponent<BossCombatLogic>();
        _combatLogic.CheckDistanceForStates();
    }
}
