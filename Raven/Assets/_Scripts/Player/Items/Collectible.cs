using System;
using Raven.Input;
using UnityEngine;
using Zenject;

namespace Raven.Player
{
    public enum CollectibleName { Dash, FireState, SecondWeapon }

    [RequireComponent(typeof(Collider))]
    public class Collectible : MonoBehaviour
    {
        [SerializeField] private CollectibleName _collectibleName;
        [Space]
        [SerializeField] private GameObject _canvas;
        [SerializeField] private Transform _infoUiTransform;
        [SerializeField] private Transform _infoUiLockTransform;

        private PlayerStatesManager _playerStatesManager;
        private InputManager _inputManager;
        private bool _canTake;

        public event Action<CollectibleName> OnUnlock;

        [Inject]
        public void Construt(PlayerStatesManager p_playerStatesManager, InputManager pInputManager)
        {
            _inputManager = pInputManager;
            _playerStatesManager = p_playerStatesManager;

            _infoUiTransform.position = Camera.main.WorldToScreenPoint(_infoUiLockTransform.position);
        }

        public void Update()
        {
            if (_canTake && _inputManager.TakeButtonPressed())
            {
                _playerStatesManager.UnlockState(_collectibleName);
                OnUnlock?.Invoke(_collectibleName);
                Destroy(this.gameObject);
            }
        }

        private void OnTriggerEnter(Collider p_collider)
        {
            if (p_collider.tag == "Player")
            {
                _canvas.SetActive(true);
                _canTake = true;
            }
        }

        private void OnTriggerExit(Collider p_collider)
        {
            if (p_collider.tag == "Player")
            {
                _canvas.SetActive(false);
                _canTake = false;
            }
        }
    }
}

