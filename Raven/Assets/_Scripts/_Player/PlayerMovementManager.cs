using Raven.Config;
using Raven.Input;
using System;
using UnityEngine;
using Zenject;

namespace Raven.Manager
{
    public class PlayerMovementManager : IFixedTickable, ITickable, IDisposable
    {
        private readonly Transform _playerTransform;
        private readonly CharacterController _playerController;
        private readonly InputController _inputController;
        private readonly MovementConfig _movementConfig;
        private readonly Transform _camTransform;
        private readonly CameraManager _cameraManager;

        private Vector3 _moveVector;
        private Vector3 _gravityVelocity;
        private float _currentGravity;
        private float _turnSmoothVelocity;
        private bool _dash;
        private float _dashTimer;
        private bool _fpp;

        public Transform PlayerTransform => _playerTransform;

        public event Action<float> OnMove;
        public event Action<bool> OnDash;

        public PlayerMovementManager(GameObject p_player, MovementConfig p_movementConfig, Transform p_camTransform, CameraManager p_cameraManager)
        {
            _playerTransform = p_player.GetComponent<Transform>();
            _playerController = p_player.GetComponent<CharacterController>();
            _inputController = p_player.GetComponent<InputController>();
            _movementConfig = p_movementConfig;
            _camTransform = p_camTransform;
            _cameraManager = p_cameraManager;

            _cameraManager.OnAimChange += SetPov;
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= SetPov;
        }

        public void Tick()
        {
            if (!_dash)
            {
                SetMoveVector();
                ActiveDash();
            }
        }

        public void FixedTick()
        {
            Gravity();

            if (!_dash)
            {
                if (_fpp)
                {
                   FppMove(_moveVector);
                }
                else
                {
                    TppMovement(_moveVector);
                }
            }
            else
            {
                Dash();
            }
        }

        private void Gravity()
        {
            if (_playerController.isGrounded)
            {
                _currentGravity += _movementConfig.GravityValue * Time.fixedDeltaTime;
            }
            else
            {
                _currentGravity = -1f;
            }

            _gravityVelocity = new Vector3(0, _currentGravity, 0);
            _playerController.Move(_gravityVelocity * Time.fixedDeltaTime);
        }

        private void SetMoveVector()
        {
            _moveVector = new Vector3(_inputController.GetMovementAxis().x, 0, _inputController.GetMovementAxis().y).normalized;
        }

        private void TppMovement(Vector3 p_moveVector)
        {
            if (p_moveVector.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(p_moveVector.x, p_moveVector.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(_playerTransform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _movementConfig.TurnSmoothTime);
                _playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                _playerController.Move(moveDir.normalized * _movementConfig.MoveSpeed * Time.deltaTime);

                OnMove?.Invoke(_movementConfig.MoveSpeed);
            }
            else
            {
                OnMove?.Invoke(0);
            }
        }

        private void FppMove(Vector3 p_moveVector)
        {
            Vector3 move = _playerTransform.right * p_moveVector.x + _playerTransform.forward * p_moveVector.z;

            _playerTransform.Rotate(Vector3.up * _inputController.GetMouseDelta().x);
            _playerController.Move(move * _movementConfig.MoveSpeed * Time.deltaTime);
        }

        private void ActiveDash()
        {
            _dash = _inputController.DashButtonPressed();
            OnDash?.Invoke(_dash);
        }

        private void Dash()
        {
            if (_dashTimer <= _movementConfig.DashTime)
            {
                _dashTimer += Time.deltaTime;

                if (_moveVector.magnitude > 0)
                {
                    _playerController.Move(_moveVector * _movementConfig.DashSpeed * Time.deltaTime);
                }
                else
                {
                    _playerController.Move(_playerTransform.forward * _movementConfig.DashSpeed * Time.deltaTime);
                }
            }
            else
            {
                _dash = false;
                _dashTimer = 0f;
                OnDash?.Invoke(_dash);
            }
        }

        private void SetPov(bool p_aim)
        {
            _fpp = p_aim;
        }
    }
}

