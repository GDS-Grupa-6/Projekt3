using System;
using Raven.Config;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Raven.UI
{
    public class PlayerHudManager : ITickable
    {
        private PlayerDataConfig _playerDataConfig;

        private Slider _energySlider;
        private Slider _healthSlider;

        private float _energyRegenerationTimer;
        private float _startEnergyRegenerationTimer;

        private bool _regenerateEnergy;

        public PlayerHudManager(Slider p_energySlider, Slider p_healthSlider, PlayerDataConfig p_playerDataConfig)
        {
            _energySlider = p_energySlider;
            _healthSlider = p_healthSlider;
            _playerDataConfig = p_playerDataConfig;

            SetSlidersValues();
        }

        public void Tick()
        {
            if (!_regenerateEnergy)
            {
                TimerToEnergyRegeneration();
            }
            else
            {
                EnergyRegeneration();
            }
        }

        public bool TrySubtractEnergy(float p_value)
        {
            if (_energySlider.value - p_value < 0)
            {
                return false;
            }

            _energySlider.value -= p_value;
            _startEnergyRegenerationTimer = 0;
            _regenerateEnergy = false;

            return true;
        }

        private void SetSlidersValues()
        {
            _energySlider.maxValue = _playerDataConfig.MaxEnergyValue;
            _energySlider.value = _playerDataConfig.MaxEnergyValue;

            _healthSlider.maxValue = _playerDataConfig.MaxHealthValue;
            _healthSlider.value = _playerDataConfig.MaxHealthValue;
        }

        private void AddEnergy(float p_value)
        {
            _energySlider.value += p_value;
        }

        private void EnergyRegeneration()
        {
            if (_energySlider.value < _energySlider.maxValue)
            {
                if (_energyRegenerationTimer < 1)
                {
                    _energyRegenerationTimer += Time.deltaTime;
                }
                else
                {
                    AddEnergy(2);
                    _energyRegenerationTimer = 0;
                }
            }
        }

        private void TimerToEnergyRegeneration()
        {
            if (_startEnergyRegenerationTimer < 2)
            {
                _startEnergyRegenerationTimer += Time.deltaTime;
            }
            else
            {
                _regenerateEnergy = true;
                _startEnergyRegenerationTimer = 0;
            }
        }
    }
}

