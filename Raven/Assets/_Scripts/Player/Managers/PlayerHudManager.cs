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
        PlayerHudReferences _playerHudReferences;

        private float _energyRegenerationTimer;
        private float _startEnergyRegenerationTimer;

        private bool _regenerateEnergy;
        private GameObject _rigTarget;

        public event Action<float> OnAddHealth;

        public PlayerHudManager(PlayerHudReferences p_hudReferences,PlayerDataConfig p_playerDataConfig, CameraManager p_cameraManager, CoroutinesManager p_coroutinesManager,
                                Player.Collectible[] p_collectibles, PlayerRigManager p_playerRigManager)
        {
            _rigTarget = p_playerRigManager.RigTarget;
            _playerHudReferences = p_hudReferences;
            _playerDataConfig = p_playerDataConfig;
            _cameraManager = p_cameraManager;
            _coroutinesManager = p_coroutinesManager;
            _collectibles = p_collectibles;

            SetSlidersValues();

            _cameraManager.OnAimChange += SetViewFinder;

            for (int i = 0; i < _collectibles.Length; i++)
            {
                _collectibles[i].OnUnlock += UnlockHud;
            }
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= SetViewFinder;

            for (int i = 0; i < _collectibles.Length; i++)
            {
                _collectibles[i].OnUnlock -= UnlockHud;
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

            _playerHudReferences.HealthCounterText.SetText($"{ _playerHudReferences.HealthSlider.value}/{ _playerHudReferences.HealthSlider.maxValue}");
            _playerHudReferences.EnergyCounterText.SetText($"{ _playerHudReferences.EnergySlider.value}/{ _playerHudReferences.EnergySlider.maxValue}");
        }

        public bool TrySubtractEnergy(float p_value)
        {
            if (_playerHudReferences.EnergySlider.value - p_value < 0)
            {
                return false;
            }

            _playerHudReferences.EnergySlider.value -= p_value;
            _startEnergyRegenerationTimer = 0;
            _regenerateEnergy = false;

            return true;
        }

        public bool TrySubtractHealth(float p_value)
        {
            if (_playerHudReferences.HealthSlider.value - p_value < 0)
            {
                return false;
            }

            _playerHudReferences.HealthSlider.value -= p_value;
            return true;
        }

        public void AddEnergy(int p_value)
        {
            if (_playerHudReferences.EnergySlider.value + p_value > _playerHudReferences.EnergySlider.maxValue)
            {
                _playerHudReferences.EnergySlider.value = _playerHudReferences.EnergySlider.maxValue;
                return;
            }

            _playerHudReferences.EnergySlider.value += p_value;
        }

        public void AddHealth(int p_value)
        {
            if (_playerHudReferences.HealthSlider.value + p_value > _playerHudReferences.HealthSlider.maxValue)
            {
                _playerHudReferences.HealthSlider.value = _playerHudReferences.HealthSlider.maxValue;
            }
            else
            {
                _playerHudReferences.HealthSlider.value += p_value;
            }
            
            OnAddHealth?.Invoke(_playerHudReferences.HealthSlider.value);
        }

        public void AddMaxHelthEnergy(int p_hAdd, int p_eAdd)
        {
            int maxH = (int)_playerHudReferences.HealthSlider.maxValue;
            int maxE = (int)_playerHudReferences.EnergySlider.maxValue;

            maxH += p_hAdd;
            maxE += p_eAdd;

            _playerHudReferences.HealthSlider.maxValue = maxH;
            _playerHudReferences.HealthSlider.value = maxH;
            _playerHudReferences.EnergySlider.maxValue = maxE;
            _playerHudReferences.EnergySlider.value = maxE;
        }

        public void ChangeStateImage(PlayerStateName p_playerStateName)
        {
            if (p_playerStateName == PlayerStateName.Fire)
            {
                _playerHudReferences.StateImage.sprite = _playerHudReferences.NormalStateSprite;
            }
            else
            {
                _playerHudReferences.StateImage.sprite = _playerHudReferences.FireSprite;
            }
        }

        private void SetSlidersValues()
        {
            _playerHudReferences.EnergySlider.maxValue = _playerDataConfig.MaxEnergyValue;
            _playerHudReferences.EnergySlider.value = _playerDataConfig.MaxEnergyValue;

            _playerHudReferences.HealthSlider.maxValue = _playerDataConfig.MaxHealthValue;
            _playerHudReferences.HealthSlider.value = _playerDataConfig.MaxHealthValue;
        }

        private void EnergyRegeneration()
        {
            if (_playerHudReferences.EnergySlider.value < _playerHudReferences.EnergySlider.maxValue)
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
                _coroutinesManager.StartCoroutine(SetViewFinderCoroutine(), _playerHudReferences.ViewFinder.gameObject);
            }
            else
            {
                _coroutinesManager.StopAllCoroutines(_playerHudReferences.ViewFinder.gameObject);
                _playerHudReferences.ViewFinder.gameObject.SetActive(false);
            }
        }

        private IEnumerator SetViewFinderCoroutine()
        {
            yield return new WaitForSeconds(0.7f);
            _playerHudReferences.ViewFinder.gameObject.SetActive(true);
        }

        private void UnlockHud(CollectibleName p_collectibleName)
        {
            switch (p_collectibleName)
            {
                case CollectibleName.Dash:
                    _playerHudReferences.DashImage.sprite = _playerHudReferences.DashSprite;
                    _playerHudReferences.DashLocked.SetActive(false);
                    _coroutinesManager.StartCoroutine(PopUpCoroutine("Dash unlocked"),this);
                    break;
                case CollectibleName.FireState:
                    _playerHudReferences.StateImage.sprite = _playerHudReferences.FireSprite;
                    _playerHudReferences.StateLocked.SetActive(false);
                    _coroutinesManager.StartCoroutine(PopUpCoroutine("Fire state unlocked"), this);
                    break;
                case CollectibleName.SecondWeapon:
                    _playerHudReferences.Weapon2Image.color = Color.white;
                    _coroutinesManager.StartCoroutine(PopUpCoroutine("Second pistol picked up"), this);
                    break;
                default:
                    break;
            }
        }

        private IEnumerator PopUpCoroutine(string p_text)
        {
            _playerHudReferences.PopUpText.SetText(p_text);
            _playerHudReferences.PopUp.SetActive(true);

            yield return new WaitForSeconds(2f);
            _playerHudReferences.PopUp.SetActive(false);
        }
    }
}

