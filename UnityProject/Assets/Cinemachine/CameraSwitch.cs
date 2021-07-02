using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

internal enum CameraID { Shoot, TPP, AIM }

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook tppCamera;
    [SerializeField] private ShootCamera shootCamera;

    private Animator _animator;
    private CharacterControllerLogic _player;
    private InputManager _inputManager;
    [HideInInspector] public bool playerIsInShootPose;
    [HideInInspector] public bool playerAim;
    [HideInInspector] public Transform targetEnemy;

    private void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _animator = GetComponent<Animator>();
        _player = FindObjectOfType<CharacterControllerLogic>();
    }

    private void Start()
    {
        _inputManager.inputSystem.Player.SwithTppShootState.performed += _ => SwitchState();
        _inputManager.inputSystem.Player.Aim.performed += _ => OnOffAim();

        playerIsInShootPose = false;
        playerAim = false;
    }

    private void SwitchState()
    {
        if (!_player.GetComponent<PlayerData>().hitTaken)
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
                _player.GetComponent<Animator>().SetBool("ChangePos", false);
            }
            else if (!playerIsInShootPose)
            {
                SwitchToCamera(CameraID.Shoot);
                _player.GetComponent<Animator>().SetBool("ChangePos", true);
            }
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
                _player.transform.rotation = Quaternion.Euler(0, tppCamera.m_XAxis.Value, 0);
                _animator.Play("ShootCamera");
                _player.animator.SetBool("ShootPos", true);
                playerIsInShootPose = true;
                shootCamera.ResetXCameraRotation();
                break;
            case CameraID.TPP:
                Vector3 pos = _player.transform.rotation.eulerAngles;
                tppCamera.m_XAxis.Value = pos.y;
                _player.animator.SetBool("ShootPos", false);
                _animator.Play("TppCamera");
                playerIsInShootPose = false;
                break;
            case CameraID.AIM:
                _player.animator.SetBool("ShootPos", true); //ewentualna zmiana na animacije AIM
                _animator.Play("AimCamera");
                playerIsInShootPose = false;
                break;
            default:
                break;
        }
    }
}
