using System;
using Raven.Input;
using Raven.Manager;
using UnityEngine;
using Zenject;

public class PlayerAnimatorManager : IDisposable, ITickable
{
    private Animator _animator;
    private PlayerMovementManager _playerMovementManager;
    private CameraManager _cameraManager;
    private InputController _inputController;

    [Inject]
    public PlayerAnimatorManager(Animator p_animator, PlayerMovementManager p_playerMovementManager, CameraManager p_cameraManager, InputController p_inputController)
    {
        _animator = p_animator;
        _playerMovementManager = p_playerMovementManager;
        _cameraManager = p_cameraManager;
        _inputController = p_inputController;

        _playerMovementManager.OnMove += SetSpeedParameter;
        _playerMovementManager.OnDash += SetDashParameter;

        _cameraManager.OnAimChange += SetAimBool;
    }

    public void Tick()
    {
        SetDirectionFloat();
    }

    public void Dispose()
    {
        _playerMovementManager.OnMove -= SetSpeedParameter;
        _playerMovementManager.OnDash -= SetDashParameter;

        _cameraManager.OnAimChange -= SetAimBool;
    }

    private void SetSpeedParameter(float p_value)
    {
        _animator.SetFloat("Speed", p_value);
    }

    private void SetDashParameter(bool p_value)
    {
        _animator.SetBool("Dash", p_value);
    }

    private void SetAimBool(bool p_aim)
    {
        _animator.SetBool("Aim", p_aim);
    }

    private void SetDirectionFloat()
    {
        _animator.SetFloat("DirectionX", _inputController.GetMovementAxis().x);
        _animator.SetFloat("DirectionY", _inputController.GetMovementAxis().y);
    }
}
