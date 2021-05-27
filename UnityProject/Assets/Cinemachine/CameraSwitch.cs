using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] private Transform shootCamera;
    [SerializeField] private CinemachineFreeLook tppCamera;

    private Animator animator;
    private CharacterControllerLogic player;
    private InputManager inputManager;
    [HideInInspector] public bool playerIsInShootPose;
    [HideInInspector] public bool playerAim;

    private void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        animator = GetComponent<Animator>();
        player = FindObjectOfType<CharacterControllerLogic>();
    }

    private void Start()
    {
        inputManager.inputSystem.Player.SwithTppShootState.performed += _ => SwitchState();
        inputManager.inputSystem.Player.Aim.performed += _ => OnOffAim();

        playerIsInShootPose = false;
    }

    private void SwitchState()
    {
        if (!playerIsInShootPose && !playerAim)
        {
            player.transform.rotation = Quaternion.Euler(0, tppCamera.m_XAxis.Value, 0);
            animator.Play("ShootCamera");
            player.animator.SetBool("ShootPos", true);
            playerIsInShootPose = true;
        }
        else if (playerIsInShootPose && !playerAim)
        {
            Vector3 pos = player.transform.rotation.eulerAngles;
            tppCamera.m_XAxis.Value = pos.y;
            player.animator.SetBool("ShootPos", false);
            animator.Play("TppCamera");
            playerIsInShootPose = false;
        }
        else if (playerIsInShootPose && playerAim)
        {
            player.animator.SetBool("ShootPos", false);
            animator.Play("AimCamera");
            playerIsInShootPose = false;
        }
    }

    private void OnOffAim()
    {
        if (playerAim)
        {
            playerAim = false;

            if (!playerIsInShootPose)
            {
                SwitchState();
            }
        }
        else if (!playerAim)
        {
            playerAim = true;

            if (!playerIsInShootPose)
            {
                player.animator.SetBool("ShootPos", true);
                animator.Play("AimCamera");
            }
        }
    }
}
