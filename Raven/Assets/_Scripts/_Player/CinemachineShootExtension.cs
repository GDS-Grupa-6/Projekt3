using System;
using Cinemachine;
using Raven.Input;
using UnityEngine;
using Zenject;

namespace Raven.Core
{
    public class CinemachineShootExtension : CinemachineExtension
    {
        [SerializeField] private float _horizontalSpeed = 10f;
        [SerializeField] private float _verticalSpeed = 10f;
        [SerializeField] private float _clampAngle = 80f;

        private InputController _inputController;
        private Vector3 _startRotation;

        [Inject]
        public void Construct(InputController p_inputController)
        {
            _inputController = p_inputController;
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
                    _startRotation.x += deltaInput.x * _verticalSpeed * Time.deltaTime;
                    _startRotation.y += deltaInput.y * _horizontalSpeed * Time.deltaTime;
                    _startRotation.y = Mathf.Clamp(_startRotation.y, -_clampAngle, _clampAngle);
                    state.RawOrientation = Quaternion.Euler(-_startRotation.y, 0f, 0f);
                }
            }
        }
    }
}

