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
    [HideInInspector] public Transform targetEnemy;

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
        targetEnemy = GameObject.FindGameObjectWithTag("Boss").transform;

        if (playerAim && targetEnemy != null)
        {
            playerAim = false;
            targetEnemy = null;

            if (!playerIsInShootPose)
            {
                SwitchToCamera(CameraID.TPP);
            }
        }
        else if (!playerAim && targetEnemy != null)
        {
            playerAim = true;

            if (!playerIsInShootPose)
            {
                SwitchToCamera(CameraID.AIM);
            }
        }
        else if (targetEnemy == null)
        {
            Debug.Log("Nie ma bossa na arenie");
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
                player.animator.SetBool("ShootPos", true); //ewentualna zmiana na animacije AIM
                animator.Play("AimCamera");
                playerIsInShootPose = false;
                break;
            default:
                break;
        }
    }
}
