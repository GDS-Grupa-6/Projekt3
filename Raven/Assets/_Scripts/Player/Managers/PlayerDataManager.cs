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
    public class PlayerDataManager
    {
        private PlayerDataConfig _config;
        private PlayerHudManager _playerHudManager;
        private PlayerMovementManager _playerMovementManager;

        private float _currentHealth;

        public PlayerDataManager(PlayerDataConfig p_config, PlayerHudManager p_playerHudManager, PlayerMovementManager p_playerMovementManager)
        {
            _playerMovementManager = p_playerMovementManager;
            _config = p_config;
            _playerHudManager = p_playerHudManager;
            _currentHealth = _config.MaxHealthValue;
        }

        public void TakeDamage(float p_value)
        {
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
            Debug.Log("PlayerDead");
        }
    }
}

