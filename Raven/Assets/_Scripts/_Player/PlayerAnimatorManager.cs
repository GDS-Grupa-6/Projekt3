using System;
using Raven.Manager;
using UnityEngine;
using Zenject;

public class PlayerAnimatorManager : IDisposable
{
    private Animator _animator;
    private PlayerMovementManager _playerMovementManager;

    [Inject]
    public PlayerAnimatorManager(Animator p_animator, PlayerMovementManager p_playerMovementManager)
    {
        _animator = p_animator;
        _playerMovementManager = p_playerMovementManager;

        _playerMovementManager.OnMove += SetSpeedParametr;
        _playerMovementManager.OnDash += SetDashParametr;
    }


    public void Dispose()
    {
        _playerMovementManager.OnMove -= SetSpeedParametr;
        _playerMovementManager.OnDash -= SetDashParametr;
    }

    private void SetSpeedParametr(float p_value)
    {
        _animator.SetFloat("Speed", p_value);
    }

    private void SetDashParametr(bool p_value)
    {
        _animator.SetBool("Dash", p_value);
    }
}
