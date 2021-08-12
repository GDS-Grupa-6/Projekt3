using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Raven.Config;
using Raven.Container;
using Raven.Core.Interface;
using Raven.Input;
using Raven.Manager;
using Raven.Player;
using Raven.UI;
using UnityEngine;
using Zenject;

namespace Raven.Player
{
    public class PlayerStatesManager : ITickable
    {
        private readonly PlayerStatesContainer _playerStatesContainer;
        private readonly InputController _inputController;
        private readonly CoroutinesManager _coroutinesManager;
        private readonly NormalState _normalState;
        private readonly FireState _fireState;
        private readonly GameObject _player;
        private readonly PlayerRigManager _playerRigManager;

        private Transform _oneHandShootPoint;
        private Transform _twoHandsShootPoint;
        private PlayerStateConfig _currentConfig;
        private IPlayerState _currentBehaviour;
        private bool _canShoot;

        public PlayerStateConfig CurrentConfig => _currentConfig;
        public IPlayerState CurrentBehaviour => _currentBehaviour;

        public PlayerStatesManager(PlayerStatesContainer p_playerStatesContainer, InputController p_inputController,
            NormalState p_normalState, FireState p_fireState, PlayerHudManager p_hudManager, CoroutinesManager p_coroutinesManager,
            GameObject p_player, PlayerRigManager p_playerRigManager, Transform p_one, Transform p_two)
        {
            _playerStatesContainer = p_playerStatesContainer;
            _inputController = p_inputController;
            _normalState = p_normalState;
            _fireState = p_fireState;
            _coroutinesManager = p_coroutinesManager;
            _player = p_player;
            _playerRigManager = p_playerRigManager;
            _oneHandShootPoint = p_one;
            _twoHandsShootPoint = p_two;

            _normalState.Initialize(p_inputController, p_hudManager, this);
            _fireState.Initialize(p_inputController, p_hudManager, this);

            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
            _currentBehaviour = _normalState;
            _canShoot = true;
        }

        public void Tick()
        {
            if (_inputController.ActiveStateButtonPressed())
            {
                if (_currentConfig.PlayerStateName == PlayerStateName.Normal)
                {
                    _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Fire);
                    _currentBehaviour = _fireState;
                }
                else
                {
                    _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
                    _currentBehaviour = _normalState;
                }
            }

            if (_inputController.ShootButtonPressed() && _canShoot)
            {
                _coroutinesManager.StartCoroutine(ShootDelay(), _player);
                _currentBehaviour.Shoot(_oneHandShootPoint,_playerRigManager.RigTarget.transform);
            }
        }

        private IEnumerator ShootDelay()
        {
            _canShoot = false;
            yield return new WaitForSeconds(_currentConfig.OneHandDelay);
            _canShoot = true;
        }
    }
}
