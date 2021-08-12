using System;
using Cinemachine;
using Raven.Input;
using Raven.Manager;
using Raven.Player;
using UnityEngine;
using Zenject;

namespace Raven.Core
{
    public class CinemachineShootExtension : CinemachineExtension
    {
        [SerializeField] private float _horizontalSpeed = 10f;
        [SerializeField] private float _clampAngle = 20f;

        private InputController _inputController;
        private Vector3 _startRotation;
        private PlayerMovementManager _playerMovementManager;
        private GameObject _cameraLock;

        [Inject]
        public void Construct(InputController p_inputController, PlayerMovementManager p_playerMovementManager, CameraManager p_cameraManager)
        {
            _inputController = p_inputController;
            _playerMovementManager = p_playerMovementManager;
            _cameraLock = p_cameraManager.ShootCameraLock;
        }

        protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
        {
            if (vcam.Follow)
            {
                if (stage == CinemachineCore.Stage.Aim)
                {
                    if (_startRotation == null)
                    {
                        _startRotation = transform.localRotation.eulerAngles;
                    }

                    Vector2 deltaInput = _inputController.GetMouseDelta();
                    _startRotation.y += deltaInput.y * _horizontalSpeed * Time.deltaTime;
                    _startRotation.y = Mathf.Clamp(_startRotation.y, -_clampAngle, _clampAngle);
                    state.RawOrientation = Quaternion.Euler(-_startRotation.y, _playerMovementManager.PlayerTransform.eulerAngles.y, 0f);
                    _cameraLock.transform.localEulerAngles = new Vector3(-_startRotation.y, 0, 0);
                }
            }
        }
    }
}

