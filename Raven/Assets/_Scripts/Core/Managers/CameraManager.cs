using Raven.Input;
using System;
using Cinemachine;
using Raven.Config;
using UnityEngine;
using Zenject;

namespace Raven.Manager
{
    public class CameraManager : ITickable, IDisposable
    {
        private InputManager _inputManager;
        private GameObject _shootCamera;
        private CinemachineFreeLook _tppCamera;
        private Transform _playerTransform;
        private Transform _mainCamera;
        private MovementConfig _movementConfig;

        private bool _setPlayerRotation;
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        public GameObject ShootCameraLock;

        public event Action<bool> OnAimChange;

        public CameraManager(InputManager pInputManager, GameObject p_shootCamera, CinemachineFreeLook p_tppCamera,
            GameObject p_player, Transform p_mainCamera, GameObject p_ShootCameraLock, MovementConfig p_movementConfig)
        {
            _movementConfig = p_movementConfig;
            _inputManager = pInputManager;
            _shootCamera = p_shootCamera;
            _tppCamera = p_tppCamera;
            _playerTransform = p_player.GetComponent<Transform>();
            _mainCamera = p_mainCamera;
            ShootCameraLock = p_ShootCameraLock;

            OnAimChange += SetCameras;
        }

        public void Dispose()
        {
            OnAimChange -= SetCameras;
        }

        public void Tick()
        {
            OnAimChange?.Invoke(_inputManager.AimButtonHold());
        }

        private void SetCameras(bool p_aim)
        {
            if (p_aim)
            {
                if (_setPlayerRotation)
                {
                    SetPlayerRotation();
                }
                else
                {
                    ShootCameraRotation();
                    _tppCamera.m_XAxis.Value = _playerTransform.eulerAngles.y;
                }
            }
            else
            {
                _setPlayerRotation = true;
            }

            _shootCamera.SetActive(p_aim);
        }

        private void ShootCameraRotation()
        {
            if (_inputManager.GetMouseDelta().sqrMagnitude >= 1f)
            {
                _cinemachineTargetYaw += _inputManager.GetMouseDelta().x * Time.deltaTime * _movementConfig.FppMouseSensitivity;
                _cinemachineTargetPitch += _inputManager.GetMouseDelta().y * Time.deltaTime * _movementConfig.FppMouseSensitivity;
            }

            _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
            _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, -45, 45);

            ShootCameraLock.transform.localRotation = Quaternion.Euler(-_cinemachineTargetPitch, 0f, 0.0f);
            _playerTransform.rotation = Quaternion.Euler(0f, _cinemachineTargetYaw, 0.0f);
        }

        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }

        private void SetPlayerRotation()
        {
            _cinemachineTargetYaw = _mainCamera.eulerAngles.y;
           _playerTransform.rotation = Quaternion.Euler(0f, _cinemachineTargetYaw, 0.0f);

            _setPlayerRotation = false;
        }
    }
}

