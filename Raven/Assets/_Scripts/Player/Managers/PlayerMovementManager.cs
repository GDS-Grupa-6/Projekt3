using Raven.Config;
using Raven.Input;
using System;
using System.Collections;
using System.Diagnostics.Eventing.Reader;
using ModestTree;
using Raven.Player;
using Raven.UI;
using UnityEngine;
using Zenject;
using System.Linq;

namespace Raven.Manager
{
    public class PlayerMovementManager : IFixedTickable, ITickable, IDisposable
    {
        private readonly Transform _playerTransform;
        private readonly CharacterController _playerController;
        private readonly InputManager _inputManager;
        private readonly MovementConfig _movementConfig;
        private readonly Transform _camTransform;
        private readonly CameraManager _cameraManager;
        private readonly CoroutinesManager _coroutinesManager;
        private readonly PlayerStatesManager _playerStatesManager;

        private Transform _groundCheck;

        private Vector3 _moveVector;
        private Vector3 _gravityVelocity;
        private float _currentGravity;
        private float _turnSmoothVelocity;
        private bool _dash;
        private float _dashTimer;
        private bool _fpp;
        private bool _fppToTppDelay;
        private bool _gravity;

        public CharacterController PlayerController => _playerController;
        public Transform PlayerTransform => _playerTransform;
        public bool Dash { get => _dash; set => _dash = value; }
        public bool GravityBool { get => _gravity; set => _gravity = value; }
        public Vector3 MoveVector => _moveVector;
        public bool Fpp => _fpp;

        public event Action<float> OnMove;
        public Action<bool> OnDash;

        [Inject]
        public PlayerMovementManager(GameObject p_player, MovementConfig p_movementConfig, Transform p_camTransform, CameraManager p_cameraManager,
            CoroutinesManager p_coroutinesManager, InputManager pInputManager, PlayerStatesManager p_playerStatesManager, Transform p_groundCheck)
        {
            _groundCheck = p_groundCheck;
            _playerTransform = p_player.GetComponent<Transform>();
            _playerController = p_player.GetComponent<CharacterController>();
            _inputManager = pInputManager;
            _movementConfig = p_movementConfig;
            _camTransform = p_camTransform;
            _cameraManager = p_cameraManager;
            _coroutinesManager = p_coroutinesManager;
            _playerStatesManager = p_playerStatesManager;

            _cameraManager.OnAimChange += SetFpp;

            _gravity = true;
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= SetFpp;
        }

        public void Tick()
        {
            if (_gravity)
            {
                Gravity();
            }

            SetMoveVector();

            if (!_dash && IsGrounded())
            {
                _playerStatesManager.CurrentBehaviour.ActiveDash(this);
            }

            if (_moveVector.magnitude > 0)
            {
                OnMove?.Invoke(_movementConfig.MoveSpeed);
            }
            else
            {
                OnMove?.Invoke(0);
            }
        }

        public void FixedTick()
        {
            if (!_dash)
            {
                if (_fpp)
                {
                    FppMove(_moveVector, _movementConfig.MoveSpeed);
                }
                else
                {
                    TppMovement(_moveVector, _movementConfig.MoveSpeed);
                }
            }
            else
            {
                _playerStatesManager.CurrentBehaviour.Dash(this);
            }
        }

        #region Movement Scripts

        private void Gravity()
        {
            if (!_playerController.isGrounded)
            {
                _currentGravity += _movementConfig.GravityValue * Time.deltaTime;
            }
            else
            {
                _currentGravity = -1f;
            }

            _gravityVelocity = new Vector3(0, _currentGravity, 0);
            _playerController.Move(_gravityVelocity * Time.deltaTime);
        }

        private void SetMoveVector()
        {
            _moveVector = new Vector3(_inputManager.GetMovementAxis().x, 0, _inputManager.GetMovementAxis().y).normalized;
        }

        public void TppMovement(Vector3 p_moveVector, float p_speed)
        {
            if (p_moveVector.magnitude > 0)
            {
                float targetAngle = Mathf.Atan2(p_moveVector.x, p_moveVector.z) * Mathf.Rad2Deg + _camTransform.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(_playerTransform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _movementConfig.TurnSmoothTime);
                _playerTransform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                _playerController.Move(moveDir.normalized * p_speed * Time.deltaTime);
            }
        }

        public void FppMove(Vector3 p_moveVector, float p_speed)
        {
            Vector3 move = _playerTransform.right * p_moveVector.x + _playerTransform.forward * p_moveVector.z;
            _playerController.Move(move * p_speed * Time.deltaTime);
        }

        #endregion

        private void SetFpp(bool p_aim)
        {
            if (p_aim)
            {
                _fpp = p_aim;
                _fppToTppDelay = true;
            }
            else
            {
                if (_fppToTppDelay)
                {
                    _fppToTppDelay = false;
                    _coroutinesManager.StartCoroutine(FppToTppDelayCoroutine());
                }
            }
        }

        private IEnumerator FppToTppDelayCoroutine()
        {
            yield return new WaitForSeconds(_movementConfig.FppToTppDelayTime);
            _fpp = false;
        }

        private bool IsGrounded()
        {
            RaycastHit[] hits = Physics.RaycastAll(_groundCheck.position, Vector3.down, 1);
            
            if (hits.Any(x => x.collider.tag == "Ground") || hits.Any(x => x.collider.tag == "Laver"))
            {
                return true;
            }

            return false;
        }
    }
}

