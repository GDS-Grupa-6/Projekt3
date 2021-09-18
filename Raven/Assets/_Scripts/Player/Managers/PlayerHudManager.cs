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
        private Player.Collectible[] _collectibles;

        private readonly Slider _energySlider;
        private readonly Slider _healthSlider;
        private readonly Image _viewFinder;
        private readonly TextMeshProUGUI _energyCounterText;
        private readonly TextMeshProUGUI _healthCounterText;

        private float _energyRegenerationTimer;
        private float _startEnergyRegenerationTimer;

        private TextMeshProUGUI[] _inputTexts;

        private bool _regenerateEnergy;
        private GameObject _rigTarget;

        public event Action<float> OnAddHealth;

        public PlayerHudManager(PlayerHudReferences p_hudReferences,PlayerDataConfig p_playerDataConfig, CameraManager p_cameraManager, CoroutinesManager p_coroutinesManager,
                                Player.Collectible[] p_collectibles, PlayerRigManager p_playerRigManager)
        {
            _rigTarget = p_playerRigManager.RigTarget;
            _energySlider = p_hudReferences.EnergySlider;
            _healthSlider = p_hudReferences.HealthSlider;
            _energyCounterText = p_hudReferences.EnergyCounterText;
            _healthCounterText = p_hudReferences.HealthCounterText;
            _playerDataConfig = p_playerDataConfig;
            _viewFinder = p_hudReferences.ViewFinder;
            _cameraManager = p_cameraManager;
            _coroutinesManager = p_coroutinesManager;
            _inputTexts = p_hudReferences.InputTexts;
            _collectibles = p_collectibles;

            SetSlidersValues();

            _cameraManager.OnAimChange += SetViewFinder;
            _cameraManager.OnAimChange += AimTextHud;

            _inputTexts[0].color = Color.grey;
            _inputTexts[2].color = Color.grey;

            for (int i = 0; i < _collectibles.Length; i++)
            {
                _collectibles[i].OnUnlock += UnlockUiText;
            }
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= SetViewFinder;
            _cameraManager.OnAimChange -= AimTextHud;

            for (int i = 0; i < _collectibles.Length; i++)
            {
                _collectibles[i].OnUnlock -= UnlockUiText;
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

            _healthCounterText.SetText($"{_healthSlider.value}/{_healthSlider.maxValue}");
            _energyCounterText.SetText($"{_energySlider.value}/{_energySlider.maxValue}");
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

        public void AddEnergy(int p_value)
        {
            if (_energySlider.value + p_value > _energySlider.maxValue)
            {
                _energySlider.value = _energySlider.maxValue;
                return;
            }

            _energySlider.value += p_value;
        }

        public void AddHealth(int p_value)
        {
            if (_healthSlider.value + p_value > _healthSlider.maxValue)
            {
                _healthSlider.value = _healthSlider.maxValue;
            }
            else
            {
                _healthSlider.value += p_value;
            }
            
            OnAddHealth?.Invoke(_healthSlider.value);
        }

        private void SetSlidersValues()
        {
            _energySlider.maxValue = _playerDataConfig.MaxEnergyValue;
            _energySlider.value = _playerDataConfig.MaxEnergyValue;

            _healthSlider.maxValue = _playerDataConfig.MaxHealthValue;
            _healthSlider.value = _playerDataConfig.MaxHealthValue;
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
               // _viewFinder.transform.position = Vector3.Lerp(_viewFinder.transform.position, Camera.main.WorldToScreenPoint(_rigTarget.transform.position), Time.deltaTime * 10);
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

        private void UnlockUiText(CollectibleName p_collectibleName)
        {
            switch (p_collectibleName)
            {
                case CollectibleName.Dash:
                    _inputTexts[0].color = Color.white;
                    break;

                case CollectibleName.FireState:
                    _inputTexts[0].color = Color.white;
                    _inputTexts[2].color = Color.white;
                    break;
            }
        }
    }
}

