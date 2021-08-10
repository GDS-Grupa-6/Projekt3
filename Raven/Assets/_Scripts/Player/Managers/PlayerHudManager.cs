using System;
using System.Collections;
using Raven.Config;
using Raven.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Raven.UI
{
    public class PlayerHudManager : ITickable, IDisposable
    {
        private readonly PlayerDataConfig _playerDataConfig;
        private CameraManager _cameraManager;
        private CoroutinesManager _coroutinesManager;

        private readonly Slider _energySlider;
        private readonly Slider _healthSlider;
        private Image _viewFinder;

        private float _energyRegenerationTimer;
        private float _startEnergyRegenerationTimer;
        private Vector2 _screenCenter;

        private bool _regenerateEnergy;

        public PlayerHudManager(Slider p_energySlider, Slider p_healthSlider, Image p_viewFinder, 
            PlayerDataConfig p_playerDataConfig, CameraManager p_cameraManager, CoroutinesManager p_coroutinesManager)
        {
            _energySlider = p_energySlider;
            _healthSlider = p_healthSlider;
            _playerDataConfig = p_playerDataConfig;
            _viewFinder = p_viewFinder;
            _cameraManager = p_cameraManager;
            _coroutinesManager = p_coroutinesManager;

            _screenCenter = new Vector2(Screen.width / 2, Screen.height / 2);
            _viewFinder.transform.position = _screenCenter;

            SetSlidersValues();

            _cameraManager.OnAimChange += SetViewFinder;
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= SetViewFinder;
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

        private void SetViewFinder(bool p_aim)
        {
            if (p_aim)
            {
                _coroutinesManager.StartCoroutine(SetViewFinderCoroutine(), _viewFinder.gameObject);
            }
            else
            {
                _coroutinesManager.StopAllCoroutines(_viewFinder.gameObject);
                _viewFinder.gameObject.SetActive(false);
            }
        }

        private IEnumerator SetViewFinderCoroutine()
        {
            yield return new WaitForSeconds(0.7f);
            _viewFinder.gameObject.SetActive(true);
        }
    }
}

