using Raven.Input;
using System;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace Raven.Manager
{
    public class CameraManager : ITickable, IDisposable
    {
        private InputController _inputController;
        private GameObject _shootCamera;
        private CinemachineFreeLook _tppCamera;
        private Transform _playerTransform;
        private Transform _mainCamera;

        private bool _setPlayerRotation;

        public GameObject RayLock;

        public event Action<bool> OnAimChange;

        public CameraManager(InputController p_inputController, GameObject p_shootCamera, CinemachineFreeLook p_tppCamera,
            GameObject p_player, Transform p_mainCamera, GameObject p_RayLock)
        {
            _inputController = p_inputController;
            _shootCamera = p_shootCamera;
            _tppCamera = p_tppCamera;
            _playerTransform = p_player.GetComponent<Transform>();
            _mainCamera = p_mainCamera;
            RayLock = p_RayLock;

            OnAimChange += SetCameras;
        }

        public void Dispose()
        {
            OnAimChange -= SetCameras;
        }

        public void Tick()
        {
            OnAimChange?.Invoke(_inputController.AimButtonHold());
        }

        private void SetCameras(bool p_aim)
        {
            if (p_aim)
            {
                if (_setPlayerRotation)
                {
                    SetPlayerRotation();
                }

                _tppCamera.m_XAxis.Value = _playerTransform.eulerAngles.y;
            }
            else
            {
                _setPlayerRotation = true;
            }

            _shootCamera.SetActive(p_aim);
        }

        private void SetPlayerRotation()
        {
            _setPlayerRotation = false;
            _playerTransform.eulerAngles = new Vector3(0f, _mainCamera.eulerAngles.y, 0f);
        }
    }
}

