using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
public class CharacterControllerLogic : MonoBehaviour
{
    [Header("Other objects")]
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private CameraSwitch _cameraSwitch;
    [Header("Movement options")]
    [SerializeField] private float _speed = 6f;
    [SerializeField] private float _tppTurnSmoothTime = 0.1f;
    [Header("Animation options")]
    [SerializeField] private float _animationSpeedDampTime = 0.1f;

    [HideInInspector] public Animator animator;

    private CharacterController _characterController;
    private float _turnSmoothVelocity;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontal = _inputManager.MovementControls().x;
        float vertical = _inputManager.MovementControls().y;
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        animator.SetFloat("Speed", direction.magnitude, _animationSpeedDampTime, Time.deltaTime);
        animator.SetFloat("Vertical", vertical);
        animator.SetFloat("Horizontal", horizontal);

        if (direction.magnitude >= 0.1f)
        {
            if (_cameraSwitch.playerIsInShootPose || _cameraSwitch.playerAim)
            {
                AimMovement(direction);
            }
            else if (!_cameraSwitch.playerIsInShootPose && !_cameraSwitch.playerAim)
            {
                TPPMovement(direction);
            }
        }
    }

    private void TPPMovement(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + _mainCamera.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, _tppTurnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        _characterController.Move(moveDir * _speed * Time.deltaTime);
    }

    private void AimMovement(Vector3 direction)
    {
        direction = _mainCamera.forward * direction.z + _mainCamera.right * direction.x;
        _characterController.Move(direction * _speed * Time.deltaTime);
    }
}
