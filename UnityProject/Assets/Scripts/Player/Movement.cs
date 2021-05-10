using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Movement : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Animator animator;
    [Tooltip("Prędkość poruszania się gracza")] [SerializeField] private float speed = 1;
    [SerializeField] private float jumpHeight = 3f;
    [Header("Camera options")]
    [SerializeField] private Transform mainCamera;
    [Tooltip("Wygładzenie obrotu gracza")] [SerializeField] private float turnSmoothTime = 0.1f;
    [Header("Gravity options")]
    [SerializeField] private float gravity = -9.81f;
    [Tooltip("Długość promienia groundCheck")] [SerializeField] private float groundDistance = 0.4f;
    [Tooltip("Nazwy masek obiektów na których może stać gracz")] [SerializeField] private LayerMask grounMask;

    private InputManager inputManager;
    private CharacterController characterController;
    private ShootCamera shootCamera;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    public bool playerIsInShootPose;
    private float xRotation;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        shootCamera = FindObjectOfType<ShootCamera>();

        Cursor.lockState = CursorLockMode.Locked;

        inputManager = InputManager.Instance;
    }

    private void Update()
    {
        Gravity();

        TPPMovement(inputManager.MovementControls());
    }

    private void Gravity()
    {
        isGrounded = Physics.CheckSphere(transform.position, groundDistance, grounMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        Jump();

        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void Jump()
    {
        if (isGrounded && inputManager.PlayerJumpedThisFrame())
        {
            if (animator.GetFloat("MoveSpeed") > 0.1f)
            {
                animator.SetTrigger("Jump");
            }

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    private void TPPMovement(Vector2 input)
    {
        Vector3 direction = new Vector3(input.x, 0f, input.y).normalized;
        animator.SetFloat("MoveSpeed", direction.magnitude);

        if (direction.magnitude >= 0.1f && !playerIsInShootPose)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else if (playerIsInShootPose)
        {
            Vector2 mouseDelta = inputManager.GetMouseDelta() * shootCamera.horizontalSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseDelta.x);

            if (direction.magnitude >= 0.1f)
            {
                ShootMovement(direction);
            }
        }
    }

    private void ShootMovement(Vector3 direction)
    {
        direction = mainCamera.forward * direction.z + mainCamera.right * direction.x;
        direction.y = 0f;
        characterController.Move(direction * Time.deltaTime * speed);
    }
}
