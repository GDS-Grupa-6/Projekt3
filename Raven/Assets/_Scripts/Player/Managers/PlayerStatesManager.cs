using System;
using System.Collections;
using System.Collections.Generic;
using Raven.Config;
using Raven.Container;
using Raven.Core.Interface;
using Raven.Input;
using Raven.Manager;
using Raven.Player;
using Raven.UI;
using UnityEngine;
using Zenject;

public class PlayerStatesManager : ITickable
{
    private PlayerFightStateConfig _currentConfig;
    private IPlayerState _currentBehaviour;
    private readonly PlayerStatesContainer _playerStatesContainer;
    private readonly InputController _inputController;

    private NormalState _normalState;
    private FireState _fireState;
    private GhostState _ghostState;
    private IceState _iceState;

    public PlayerFightStateConfig CurrentConfig => _currentConfig;
    public IPlayerState CurrentBehaviour => _currentBehaviour;

    public PlayerStatesManager(PlayerStatesContainer p_playerStatesContainer, InputController p_inputController, NormalState p_normalState,
        FireState p_fireState, GhostState p_ghostState, IceState p_iceState, PlayerHudManager p_hudManager)
    {
        _playerStatesContainer = p_playerStatesContainer;
        _inputController = p_inputController;
        _normalState = p_normalState;
        _fireState = p_fireState;
        _ghostState = p_ghostState;
        _iceState = p_iceState;

        _normalState.Initialize(p_inputController, p_hudManager, this);
        _fireState.Initialize(p_inputController, p_hudManager, this);
        _ghostState.Initialize(p_inputController, p_hudManager, this);
        _iceState.Initialize(p_inputController, p_hudManager, this);

        _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
        _currentBehaviour = _normalState;
    }

    public void Tick()
    {
        if (_inputController.State1ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
            _currentBehaviour = _normalState;
        }

        if (_inputController.State2ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Fire);
            _currentBehaviour = _fireState;
        }

        if (_inputController.State3ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Ghost);
            _currentBehaviour = _ghostState;
        }

        if (_inputController.State4ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Ice);
            _currentBehaviour = _iceState;
        }
    }
}
