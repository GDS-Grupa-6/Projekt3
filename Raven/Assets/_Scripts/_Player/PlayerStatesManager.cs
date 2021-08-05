using System;
using System.Collections;
using System.Collections.Generic;
using Raven.Config;
using Raven.Container;
using Raven.Input;
using UnityEngine;
using Zenject;

public class PlayerStatesManager : ITickable
{
    private PlayerFightStateConfig _currentConfig;
    private PlayerStatesContainer _playerStatesContainer;
    private InputController _inputController;

    public PlayerFightStateConfig CurrentConfig => _currentConfig;

    public PlayerStatesManager(PlayerStatesContainer p_playerStatesContainer, InputController p_inputController)
    {
        _playerStatesContainer = p_playerStatesContainer;
        _inputController = p_inputController;

        _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
    }

    public void Tick()
    {
        if (_inputController.State1ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
        }

        if (_inputController.State1ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Fire);
        }

        if (_inputController.State1ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Ghost);
        }

        if (_inputController.State1ButtonPressed())
        {
            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Ice);
        }
    }
}
