using Raven.Config;
using Raven.Core.Interface;
using Raven.Input;
using Raven.Manager;
using Raven.UI;
using UnityEngine;
using Zenject;

namespace Raven.Player
{
    public class NormalState : IPlayerState
    {
        private InputController _inputController;
        private PlayerHudManager _hudManager;
        private PlayerStatesManager _playerStatesManager;

        private float _dashTimer;

        public void Initialize(InputController p_inputController, PlayerHudManager p_hudManager,
            PlayerStatesManager p_playerStatesManager)
        {
            _playerStatesManager = p_playerStatesManager;
            _inputController = p_inputController;
            _hudManager = p_hudManager;
        }

        public void ActiveDash(PlayerMovementManager p_movementManager)
        {
            if (!_inputController.DashButtonPressed()) return;
            if (!_hudManager.TrySubtractEnergy(_playerStatesManager.CurrentConfig.DashCost)) return;

            p_movementManager.Dash = true;
            p_movementManager.OnDash?.Invoke(true);
        }

        public void Dash(PlayerMovementManager p_movementManager)
        {
            _dashTimer += Time.deltaTime;
            p_movementManager.GravityBool = false;

            if (_dashTimer > _playerStatesManager.CurrentConfig.DashTime)
            {
                p_movementManager.Dash = false;
                p_movementManager.GravityBool = true;
                _dashTimer = 0f;
                p_movementManager.OnDash?.Invoke(false);
            }

            DashMove(p_movementManager);
        }

        private void DashMove(PlayerMovementManager p_movementManager)
        {
            if (p_movementManager.MoveVector.magnitude > 0)
            {
                if (p_movementManager.Fpp)
                {
                    p_movementManager.FppMove(p_movementManager.MoveVector, _playerStatesManager.CurrentConfig.DashSpeed);
                }
                else
                {
                    p_movementManager.TppMovement(p_movementManager.MoveVector, _playerStatesManager.CurrentConfig.DashSpeed);
                }
            }
            else
            {
                p_movementManager.PlayerController.Move(p_movementManager.PlayerTransform.forward *
                                                        _playerStatesManager.CurrentConfig.DashSpeed * Time.deltaTime);
            }
        }
    }
}

