using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Movement player;
    [Header("Switch camera input")]
    [SerializeField] private InputAction action;
    [Header("Cameras")]
    [SerializeField] private Transform shootCamera;
    [SerializeField] private Transform tppCamera;
    [SerializeField] private Vector2 camerasDelta;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        action.performed += _ => SwitchState();

        player.playerIsShooting = false;
    }

    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }

    private void SwitchState()
    {
        if (!player.playerIsShooting)
        {
            animator.Play("ShootCamera");
            player.playerIsShooting = true;
        }
        else
        {
            player.playerIsShooting = false;
            animator.Play("TppCamera");
        }
    }
}
