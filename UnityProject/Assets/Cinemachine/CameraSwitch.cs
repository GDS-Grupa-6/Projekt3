using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private InputAction action;
    [SerializeField] private Movement player;
    [SerializeField] private ShootCamera shootCamera;

    private Animator animator;
    private bool tppCamera;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        action.performed += _ => SwitchState();

        tppCamera = true;
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
        if (tppCamera)
        {
            player.playerIsShooting = true;
            animator.Play("ShootCamera");
        }
        else
        {
            player.playerIsShooting = false;
            animator.Play("TppCamera");
        }

        tppCamera = !tppCamera;
    }
}
