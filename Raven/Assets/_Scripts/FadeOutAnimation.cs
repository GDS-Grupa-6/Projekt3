using Raven.Input;
using UnityEngine;
using Zenject;

public class FadeOutAnimation : StateMachineBehaviour
{
    private InputManager _inputManager;

    [Inject]
    public void Construct(InputManager p_inputManager)
    {
        _inputManager = p_inputManager;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputManager.CanInput = true;
    }
}
