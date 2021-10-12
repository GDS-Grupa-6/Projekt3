using Raven.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class FadeInAnimation : StateMachineBehaviour
{
    private InputManager _inputManager;

    [Inject]
    public void Construct(InputManager p_inputManager)
    {
        _inputManager = p_inputManager;
    }

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _inputManager.CanInput = false;
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        SceneManager.LoadScene(0);
    }
}
