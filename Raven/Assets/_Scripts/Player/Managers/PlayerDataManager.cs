using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using Raven.Config;
using Raven.Manager;
using Raven.UI;
using UnityEngine;

namespace Raven.Player
{
    public class PlayerDataManager: IDisposable
    {
        private PlayerDataConfig _config;
        private PlayerHudManager _playerHudManager;
        private PlayerMovementManager _playerMovementManager;
        private Animator _deadPanelAnimator;

        private float _currentHealth;

        public event Action OnTakeDamage;
        public event Action OnDead;

        public PlayerDataManager(PlayerDataConfig p_config, PlayerHudManager p_playerHudManager, PlayerMovementManager p_playerMovementManager, Animator p_deadPanelAnimator)
        {
            _playerMovementManager = p_playerMovementManager;
            _config = p_config;
            _playerHudManager = p_playerHudManager;
            _currentHealth = _config.MaxHealthValue;
            _deadPanelAnimator = p_deadPanelAnimator;

            _playerHudManager.OnAddHealth += SetCurrentHealth;
        }

        public void Dispose()
        {
            _playerHudManager.OnAddHealth -= SetCurrentHealth;
        }

        private void SetCurrentHealth(float p_value)
        {
            _currentHealth = p_value;
        }

        public void TakeDamage(float p_value)
        {
            OnTakeDamage?.Invoke();

            if (!_playerMovementManager.Dash)
            {
                if (_currentHealth - p_value <= 0)
                {
                    _playerHudManager.TrySubtractHealth(_currentHealth);
                    Dead();
                    return;
                }

                _currentHealth -= p_value;
                _playerHudManager.TrySubtractHealth(p_value);
            }
        }

        private void Dead()
        {
            OnDead?.Invoke();
            _deadPanelAnimator.SetTrigger("FadeIn");
        }
    }
}

