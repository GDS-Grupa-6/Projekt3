using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

internal enum CameraID { Shoot, TPP, AIM }

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
        if (playerIsInShootPose)
        {
            if (playerAim)
            {
                SwitchToCamera(CameraID.AIM);
            }
            else if (!playerAim)
            {
                SwitchToCamera(CameraID.TPP);
            }
        }
        else if (!playerIsInShootPose)
        {
            SwitchToCamera(CameraID.Shoot);
        }
    }

    private void OnOffAim()
    {
        if (playerAim)
        {
            if (!playerIsInShootPose)
            {
                playerAim = false;
                SwitchToCamera(CameraID.TPP);
            }
            else if(playerIsInShootPose)
            {
                playerAim = false;
            }
        }
        else if(!playerAim)
        {
            if (!playerIsInShootPose)
            {
                playerAim = true;
                SwitchToCamera(CameraID.AIM);
            }
            else if (playerIsInShootPose)
            {
                playerAim = true;
            }
        }
    }

    private void SwitchToCamera(CameraID camera)
    {
        switch (camera)
        {
            case CameraID.Shoot:
                player.transform.rotation = Quaternion.Euler(0, tppCamera.m_XAxis.Value, 0);
                animator.Play("ShootCamera");
                player.animator.SetBool("ShootPos", true);
                playerIsInShootPose = true;
                break;
            case CameraID.TPP:
                Vector3 pos = player.transform.rotation.eulerAngles;
                tppCamera.m_XAxis.Value = pos.y;
                player.animator.SetBool("ShootPos", false);
                animator.Play("TppCamera");
                playerIsInShootPose = false;
                break;
            case CameraID.AIM:
                //ustawienie rotacji gracza
                player.animator.SetBool("ShootPos", false);
                animator.Play("AimCamera");
                playerIsInShootPose = false;
                break;
            default:
                break;
        }
    }
}
