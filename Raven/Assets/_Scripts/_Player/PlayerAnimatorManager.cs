using System;
using Raven.Manager;
using UnityEngine;
using Zenject;

public class PlayerAnimatorManager : IDisposable
{
    private Animator _animator;
    private PlayerMovementManager _playerMovementManager;
    private CameraManager _cameraManager;

    [Inject]
    public PlayerAnimatorManager(Animator p_animator, PlayerMovementManager p_playerMovementManager, CameraManager p_cameraManager)
    {
        _animator = p_animator;
        _playerMovementManager = p_playerMovementManager;
        _cameraManager = p_cameraManager;

        _playerMovementManager.OnMove += SetSpeedParametr;
        _playerMovementManager.OnDash += SetDashParametr;

        _cameraManager.OnAimChange += SetAimBool;
    }


    public void Dispose()
    {
        _playerMovementManager.OnMove -= SetSpeedParametr;
        _playerMovementManager.OnDash -= SetDashParametr;

        _cameraManager.OnAimChange -= SetAimBool;
    }

    private void SetSpeedParametr(float p_value)
    {
        _animator.SetFloat("Speed", p_value);
    }

    private void SetDashParametr(bool p_value)
    {
        _animator.SetBool("Dash", p_value);
    }

    private void SetAimBool(bool p_aim)
    {
        _animator.SetBool("Aim", p_aim);
    }
}
