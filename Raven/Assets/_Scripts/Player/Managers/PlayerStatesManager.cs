using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        private readonly InputManager _inputManager;
        private readonly CoroutinesManager _coroutinesManager;
        private readonly NormalState _normalState;
        private readonly FireState _fireState;
        private readonly PlayerRigManager _playerRigManager;
        private readonly PlayerHudManager _playerHudManager;
        private readonly PlayerReferences _playerReferences;

        private PlayerStateConfig _currentConfig;
        private IPlayerState _currentBehaviour;
        private bool _canShoot;
        private bool _leftHandShoot;

        private GameObject[] _fireStateVfx;
        private GameObject[] _normalStateVfx;

        private Dictionary<CollectibleName, bool> _unlockedStates = new Dictionary<CollectibleName, bool>();

        public PlayerStateConfig CurrentConfig => _currentConfig;
        public IPlayerState CurrentBehaviour => _currentBehaviour;
        public Dictionary<CollectibleName, bool> UnlockedStates => _unlockedStates;

        public event Action OnShoot;
        public event Action<PlayerStateName> OnChangeState;

        public PlayerStatesManager(PlayerStatesContainer p_playerStatesContainer, InputManager pInputManager,
            NormalState p_normalState, FireState p_fireState, PlayerHudManager p_hudManager, CoroutinesManager p_coroutinesManager,
            PlayerRigManager p_playerRigManager, PlayerReferences p_playerReferences)
        {
            _playerReferences = p_playerReferences;
            _playerStatesContainer = p_playerStatesContainer;
            _inputManager = pInputManager;
            _normalState = p_normalState;
            _fireState = p_fireState;
            _coroutinesManager = p_coroutinesManager;
            _playerRigManager = p_playerRigManager;
            _playerHudManager = p_hudManager;

            _normalState.Initialize(pInputManager, p_hudManager, this);
            _fireState.Initialize(pInputManager, p_hudManager, this);

            _unlockedStates.Add(CollectibleName.Dash, false);
            _unlockedStates.Add(CollectibleName.FireState, false);
            _unlockedStates.Add(CollectibleName.SecondWeapon, false);

            _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
            _currentBehaviour = _normalState;
            _canShoot = true;
            _playerReferences.SecondWeapon.SetActive(false);
            SetStateVfx(PlayerStateName.Normal);
        }

        public void Tick()
        {
            if (_inputManager.ActiveStateButtonPressed() && _unlockedStates[CollectibleName.FireState])
            {
                ChangeState();
            }

            if (_currentBehaviour == _fireState && !_unlockedStates[CollectibleName.FireState])
            {
                return;
            }

            if (_inputManager.ShootButtonPressed() && _inputManager.AimButtonHold() && _canShoot)
            {
                _coroutinesManager.StartCoroutine(ShootDelay(), _playerReferences.Player);

                if (_unlockedStates[CollectibleName.SecondWeapon])
                {
                    if (_leftHandShoot)
                    {
                        _currentBehaviour.Shoot(_playerReferences.TwoHandsShootPoint, _playerRigManager.RigTarget.transform);
                    }
                    else
                    {
                        _currentBehaviour.Shoot(_playerReferences.OneHandShootPoint, _playerRigManager.RigTarget.transform);
                    }
                }
                else
                {
                    _currentBehaviour.Shoot(_playerReferences.OneHandShootPoint, _playerRigManager.RigTarget.transform);
                }

                OnShoot?.Invoke();
            }
        }

        private IEnumerator ShootDelay()
        {
            _canShoot = false;

            if (_unlockedStates[CollectibleName.SecondWeapon])
            {
                yield return new WaitForSeconds(_currentConfig.TwoHandsDelay);
                _leftHandShoot = _leftHandShoot == false ? true : false;
            }
            else
            {
                yield return new WaitForSeconds(_currentConfig.OneHandDelay);
            }
         
            _canShoot = true;
        }

        public void ChangeState()
        {
            if (_currentConfig.PlayerStateName == PlayerStateName.Normal)
            {
                _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Fire);
                _currentBehaviour = _fireState;
                _coroutinesManager.StartCoroutine(EnergySubtractCoroutine(), _playerReferences.Player);
            }
            else
            {
                _currentConfig = _playerStatesContainer.FindStateConfig(PlayerStateName.Normal);
                _currentBehaviour = _normalState;
            }

            SetStateVfx(_currentConfig.PlayerStateName);

            OnChangeState?.Invoke(_currentConfig.PlayerStateName);

            _playerHudManager.ChangeStateImage(_currentConfig.PlayerStateName);
        }

        public void UnlockState(CollectibleName p_collectibleName)
        {
            _unlockedStates[p_collectibleName] = true;

            if (p_collectibleName == CollectibleName.SecondWeapon)
            {
                _playerRigManager.SecondWeapon = true;
                _playerReferences.SecondWeapon.SetActive(true);
            }
        }

        private IEnumerator EnergySubtractCoroutine()
        {
            while (_currentBehaviour == _fireState)
            {
                if (!_playerHudManager.TrySubtractEnergy(2f))
                {
                    ChangeState();
                }
                yield return new WaitForSeconds(1f);
            }
        }

        private void SetStateVfx(PlayerStateName p_playerStateName)
        {
            switch (p_playerStateName)
            {
                case PlayerStateName.Normal:
                    for (int i = 0; i < _playerReferences.NorrmalStateVfx.Length; i++)
                    {
                        _playerReferences.NorrmalStateVfx[i].SetActive(true);
                    }

                    for (int i = 0; i < _playerReferences.FireStateVfx.Length; i++)
                    {
                        _playerReferences.FireStateVfx[i].SetActive(false);
                    }
                    break;

                case PlayerStateName.Fire:
                    for (int i = 0; i < _playerReferences.NorrmalStateVfx.Length; i++)
                    {
                        _playerReferences.NorrmalStateVfx[i].SetActive(false);
                    }

                    for (int i = 0; i < _playerReferences.FireStateVfx.Length; i++)
                    {
                        _playerReferences.FireStateVfx[i].SetActive(true);
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
