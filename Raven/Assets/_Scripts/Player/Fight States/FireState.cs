using Raven.Config;
using Raven.Core.Interface;
using Raven.Input;
using Raven.Manager;
using Raven.UI;
using UnityEngine;

namespace Raven.Player
{
    public class FireState : IPlayerState
    {
        private InputManager _inputManager;
        private PlayerHudManager _hudManager;
        private PlayerStatesManager _playerStatesManager;

        private float _dashTimer;

        public void Initialize(InputManager pInputManager, PlayerHudManager p_hudManager,
            PlayerStatesManager p_playerStatesManager)
        {
            _playerStatesManager = p_playerStatesManager;
            _inputManager = pInputManager;
            _hudManager = p_hudManager;
        }

        public void Shoot(Transform p_shootPoint, Transform p_lookAt)
        {
            var obj = GameObject.Instantiate(_playerStatesManager.CurrentConfig.BulletPrefab);
            obj.GetComponent<Bullet>().Initialization(_playerStatesManager);
            obj.transform.position = p_shootPoint.position;
            obj.transform.LookAt(p_lookAt);
        }

        public void ActiveDash(PlayerMovementManager p_movementManager)
        {
            if (_playerStatesManager.UnlockedStates[CollectibleName.FireState])
            {
                if (!_inputManager.DashButtonPressed()) return;
                if (!_hudManager.TrySubtractEnergy(_playerStatesManager.CurrentConfig.DashCost)) return;

                p_movementManager.Dash = true;
                p_movementManager.OnDash?.Invoke(true);
            }
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
                GenerateEffect(p_movementManager);
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

        private void GenerateEffect(PlayerMovementManager p_movementManager)
        {
            var obj = GameObject.Instantiate(_playerStatesManager.CurrentConfig.EffectPrefab);
            obj.transform.position = p_movementManager.PlayerTransform.position;
            obj.GetComponent<DashEffect>().Initialize(_playerStatesManager.CurrentConfig);
        }
    }
}

