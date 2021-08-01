using System.Collections;
using System.Collections.Generic;
using Raven.Config;
using Raven.Input;
using UnityEngine;
using Zenject;

namespace Raven.Manager
{
    public class PlayerMovementManager : IFixedTickable, ITickable
    {
        private Transform _playerTransform;
        private CharacterController _playerController;
        private InputController _inputController;
        private MovementConfig _movementConfig;
        private Transform _camTransform;

        private Vector3 _moveVector;
        private float _turnSmoothVelocity;

        public PlayerMovementManager(GameObject p_player, MovementConfig p_movementConfig, Transform p_camTransform)
        {
            _playerTransform = p_player.GetComponent<Transform>();
            _playerController = p_player.GetComponent<CharacterController>();
            _inputController = p_player.GetComponent<InputController>();
            _movementConfig = p_movementConfig;
            _camTransform = p_camTransform;
        }

        public void Tick()
        {
            SetMoveVector();
        }

        public void FixedTick()
        {
            TppMovement(_moveVector);
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
            }
        }
    }
}

