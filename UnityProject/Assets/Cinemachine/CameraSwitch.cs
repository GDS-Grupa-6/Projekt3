using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private CharacterController player;
    [Header("Switch camera input")]
    [SerializeField] private InputAction action;
    [Header("Cameras")]
    [SerializeField] private Transform shootCamera;
    [SerializeField] private CinemachineFreeLook tppCamera;

    private Animator animator;
    [HideInInspector] public bool playerIsInShootPose;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        action.performed += _ => SwitchState();

       playerIsInShootPose = false;
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
        if (!playerIsInShootPose)
        {
            player.transform.rotation = Quaternion.Euler(0, tppCamera.m_XAxis.Value, 0);
            animator.Play("ShootCamera");
            playerIsInShootPose = true;
        }
        else
        {
            Vector3 pos = player.transform.rotation.eulerAngles;
            tppCamera.m_XAxis.Value = pos.y;
            animator.Play("TppCamera");
            playerIsInShootPose = false;
        }
    }
}
