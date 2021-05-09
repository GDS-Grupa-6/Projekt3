using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Movement player;
    [Header("Switch camera input")]
    [SerializeField] private InputAction action;
    [Header("Cameras")]
    [SerializeField] private Transform shootCamera;
    [SerializeField] private CinemachineFreeLook tppCamera;
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
            player.transform.rotation = Quaternion.Euler(0, tppCamera.m_XAxis.Value, 0);
            animator.Play("ShootCamera");
            player.playerIsShooting = true;
        }
        else
        {
            player.playerIsShooting = false;
            Vector3 pos = player.transform.rotation.eulerAngles;
            Debug.Log(pos);
            tppCamera.m_XAxis.Value = pos.y;
            Debug.Log(tppCamera.m_XAxis.Value);
            animator.Play("TppCamera");
        }
    }
}
