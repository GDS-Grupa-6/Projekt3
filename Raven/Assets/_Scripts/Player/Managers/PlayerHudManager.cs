using System;
using System.Collections;
using Raven.Config;
using Raven.Manager;
using Raven.Player;
using TMPro;
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
        private Collectible[] _collectibles;

        private readonly Slider _energySlider;
        private readonly Slider _healthSlider;
        private Image _viewFinder;

        private float _energyRegenerationTimer;
        private float _startEnergyRegenerationTimer;

        private TextMeshProUGUI[] _inputTexts;

        private bool _regenerateEnergy;
        private GameObject _rigTarget;

        public PlayerHudManager(Slider p_energySlider, Slider p_healthSlider, Image p_viewFinder,
            PlayerDataConfig p_playerDataConfig, CameraManager p_cameraManager, CoroutinesManager p_coroutinesManager,
            TextMeshProUGUI[] p_inputTexts, Collectible[] p_collectibles, PlayerRigManager p_playerRigManager)
        {
            _rigTarget = p_playerRigManager.RigTarget;
            _energySlider = p_energySlider;
            _healthSlider = p_healthSlider;
            _playerDataConfig = p_playerDataConfig;
            _viewFinder = p_viewFinder;
            _cameraManager = p_cameraManager;
            _coroutinesManager = p_coroutinesManager;
            _inputTexts = p_inputTexts;
            _collectibles = p_collectibles;

            SetSlidersValues();

            _cameraManager.OnAimChange += SetViewFinder;
            _cameraManager.OnAimChange += AimTextHud;

            _inputTexts[0].color = Color.grey;
            _inputTexts[2].color = Color.grey;

            for (int i = 0; i < _collectibles.Length; i++)
            {
                _collectibles[i].OnUnlock += UnlockText;
            }
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= SetViewFinder;
            _cameraManager.OnAimChange -= AimTextHud;

            for (int i = 0; i < _collectibles.Length; i++)
            {
                _collectibles[i].OnUnlock -= UnlockText;
            }
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

        public bool TrySubtractHealth(float p_value)
        {
            if (_healthSlider.value - p_value < 0)
            {
                return false;
            }

            _healthSlider.value -= p_value;
            return true;
        }

        private void SetSlidersValues()
        {
            _energySlider.maxValue = _playerDataConfig.MaxEnergyValue;
            _energySlider.value = _playerDataConfig.MaxEnergyValue;

            _healthSlider.maxValue = _playerDataConfig.MaxHealthValue;
            _healthSlider.value = _playerDataConfig.MaxHealthValue;
        }

        private void AddEnergy(int p_value)
        {
            _energySlider.value += p_value;
        }

        private void EnergyRegeneration()
        {
            if (_energySlider.value < _energySlider.maxValue)
            {
                if (_energyRegenerationTimer < _playerDataConfig.RegenerationTime)
                {
                    _energyRegenerationTimer += Time.deltaTime;
                }
                else
                {
                    AddEnergy(_playerDataConfig.RegenerationValue);
                    _energyRegenerationTimer = 0;
                }
            }
        }

        private void TimerToEnergyRegeneration()
        {
            if (_startEnergyRegenerationTimer < _playerDataConfig.TimeToStartRegeneration)
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
                _viewFinder.transform.position = Camera.main.WorldToScreenPoint(_rigTarget.transform.position);
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

        private void AimTextHud(bool p_aim)
        {
            if (p_aim)
            {
                _inputTexts[1].SetText("LPM");
            }
            else
            {
                _inputTexts[1].SetText("PPM");
            }
        }

        private void UnlockText(CollectibleName p_collectibleName)
        {
            switch (p_collectibleName)
            {
                case CollectibleName.Dash:
                    _inputTexts[0].color = Color.white;
                    break;

                case CollectibleName.FireDash:
                    _inputTexts[0].color = Color.white;
                    _inputTexts[2].color = Color.white;
                    break;

                case CollectibleName.FireShoot:
                    _inputTexts[2].color = Color.white;

                    break;
            }
        }
    }
}

