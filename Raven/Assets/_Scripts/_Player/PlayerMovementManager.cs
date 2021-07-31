using System.Collections;
using System.Collections.Generic;
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

        private Vector3 _moveVector;

        public PlayerMovementManager(GameObject p_player)
        {
            _playerTransform = p_player.GetComponent<Transform>();
            _playerController = p_player.GetComponent<CharacterController>();
            _inputController = p_player.GetComponent<InputController>();
        }

        public void Tick()
        {
            SetMoveVector();
        }

        public void FixedTick()
        {
            MovePlayer();
        }

        private void SetMoveVector()
        {
            _moveVector = new Vector3(_inputController.GetMovementAxis().x, 0, _inputController.GetMovementAxis().y).normalized;
            Debug.Log(_inputController.GetMovementAxis());
        }

        private void MovePlayer()
        {
            _playerController.Move(_moveVector * 10 * Time.deltaTime);
        }
    }
}

