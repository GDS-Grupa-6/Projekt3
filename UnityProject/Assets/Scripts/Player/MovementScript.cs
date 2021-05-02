using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum LookMode { FPP, TPP, BOTH }

[RequireComponent(typeof(CharacterController))]
public class MovementScript : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private LookMode lookMode;
    [Tooltip("Prędkość poruszania się gracza")] [SerializeField] private float speed = 1;
    [SerializeField] private float jumpHeight = 3f;
    [Header("TPP options")]
    [SerializeField] private Transform tppCam;
    [Tooltip("Wygładzenie obrotu gracza")] [SerializeField] private float turnSmoothTime = 0.1f;
    [Header("FPP options")]
    [SerializeField] private GameObject fppCam;
    [Header("Gravity options")]
    [SerializeField] private float gravity = -9.81f;
    [Tooltip("Obiek który sprawdza czy gracz jest na ziemi")] [SerializeField] private Transform groundCheck;
    [Tooltip("Długość promienia groundCheck")] [SerializeField] private float groundDistance = 0.4f;
    [Tooltip("Nazwy masek obiektów na których może stać gracz")] [SerializeField] private LayerMask grounMask;

    private CharacterController characterController;
    private float turnSmoothVelocity;
    private bool isFPPLook = false;
    private Vector3 velocity;
    private bool isGrounded;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        ChangePlayerLook();

        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            ChangePlayerLook();
        }

        GravityAndJumping();

        if (isFPPLook)
        {
            FPPMovement();
        }
        else
        {
            TPPMovement();
        }
    }

    private void GravityAndJumping()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, grounMask);
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
    }

    private void TPPMovement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + tppCam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            characterController.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }

    private void FPPMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        characterController.Move(move * speed * Time.deltaTime);
    }

    private void ChangePlayerLook()
    {
        switch (lookMode)
        {
            case LookMode.TPP:
                isFPPLook = true;
                isFPPLook = false;
                fppCam.SetActive(false);
                tppCam.gameObject.SetActive(true);
                break;

            case LookMode.FPP:
                isFPPLook = false;
                tppCam.gameObject.SetActive(false);
                fppCam.SetActive(true);
                isFPPLook = true;
                break;

            case LookMode.BOTH:
                if (isFPPLook)
                {
                    isFPPLook = false;
                    fppCam.SetActive(false);
                    tppCam.gameObject.SetActive(true);
                }
                else
                {
                    tppCam.gameObject.SetActive(false);
                    fppCam.SetActive(true);
                    isFPPLook = true;
                }
                break;

            default:
                break;
        }

    }
}
