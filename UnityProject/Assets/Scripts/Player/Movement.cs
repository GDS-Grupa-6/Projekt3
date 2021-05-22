using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Animator animator;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float jumpHeight = 3f;
    [Header("Camera options")]
    [SerializeField] private Transform mainCamera;
    [Tooltip("Wygładzenie obrotu gracza")] [SerializeField] private float turnSmoothTime = 0.1f;
    [Header("Gravity options")]
    [SerializeField] private float gravity = -9.81f;
    [Tooltip("Długość promienia groundCheck")] [SerializeField] private float groundDistance = 0.4f;
    [Tooltip("Nazwy masek obiektów na których może stać gracz")] [SerializeField] private LayerMask grounMask;

    [HideInInspector] public bool playerIsInShootPose;
    [HideInInspector] public Vector3 moveDirection;
    [HideInInspector] public bool offMove;

    private InputManager inputManager;
    private CharacterController characterController;
    private ShootCamera shootCamera;
    private float turnSmoothVelocity;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation;
    private float speed;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        shootCamera = FindObjectOfType<ShootCamera>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        inputManager = FindObjectOfType<InputManager>();
        inputManager.inputSystem.Player.Sprint.performed += _ => SetPlayerSpeedValue(sprintSpeed);
        inputManager.inputSystem.Player.Sprint.canceled += _ => SetPlayerSpeedValue(walkSpeed);

        speed = walkSpeed;
    }

    private void Update()
    {
        if (!offMove)
        {
            Gravity();
            TPPMovement(inputManager.MovementControls());

            Debug.Log(inputManager.MovementControls().x);

            animator.SetFloat("vertical", inputManager.MovementControls().y);
            animator.SetFloat("horizontal", inputManager.MovementControls().x);
        }
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

    private void SetPlayerSpeedValue(float value)
    {
        if (value == sprintSpeed)
        {
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }

        speed = value;
    }

    private void TPPMovement(Vector2 input)
    {
        Vector3 move = new Vector3(input.x, 0f, input.y).normalized;
        animator.SetFloat("MoveSpeed", move.magnitude);

        if (move.magnitude >= 0.1f && !playerIsInShootPose)
        {
            float targetAngle = Mathf.Atan2(move.x, move.z) * Mathf.Rad2Deg + mainCamera.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDirection.normalized * speed * Time.deltaTime);
        }
        else if (playerIsInShootPose)
        {
            Vector2 mouseDelta = inputManager.GetMouseDelta() * shootCamera.horizontalSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up * mouseDelta.x);

            if (move.magnitude >= 0.1f)
            {
                ShootMovement(move);
            }
        }
    }

    private void ShootMovement(Vector3 direction)
    {
        moveDirection = mainCamera.forward * direction.z + mainCamera.right * direction.x;
        moveDirection.y = 0f;
        characterController.Move(moveDirection * Time.deltaTime * speed);
    }
}
