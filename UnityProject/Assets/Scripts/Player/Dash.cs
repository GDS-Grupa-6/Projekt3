using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(CharacterControllerLogic))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 40f;
    [SerializeField] private float dashTime = 0.25f;
    [SerializeField] private float cooldownTime = 1f;
    [SerializeField] private GameObject ghostFormVFX;
    [SerializeField] private Transform mainCam;

    [HideInInspector] public bool playerDashing;
    private CharacterController _characterController;
    private InputManager _inputManager;
    private Animator _animator;
    private CharacterControllerLogic _characterControllerLogic;
    private CameraSwitch _cameraSwitch;
    private PlayerData _playerData;

    void Start()
    {
        _playerData = GetComponent<PlayerData>();
        _characterControllerLogic = GetComponent<CharacterControllerLogic>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();
        _inputManager = FindObjectOfType<InputManager>();
        _cameraSwitch = FindObjectOfType<CameraSwitch>();
        ghostFormVFX.SetActive(false);
    }

    void Update()
    {
        if (_inputManager.PlayerDash() && !playerDashing && !_playerData.hitTaken)
        {
            _characterControllerLogic.enabled = false;
            playerDashing = true;
            StartCoroutine(DashCourutine());
        }
    }

    IEnumerator DashCourutine()
    {
        float startTime = Time.time;
        ghostFormVFX.SetActive(true);
        _animator.SetBool("ChangePos", true);

        while (Time.time < startTime + dashTime)
        {
            float horizontal = _inputManager.MovementControls().x;
            float vertical = _inputManager.MovementControls().y;

            if (_cameraSwitch.playerIsInShootPose || _cameraSwitch.playerAim)
            {
                Vector3 move = transform.right * horizontal + transform.forward * vertical;
                _characterController.Move(move * dashSpeed * Time.deltaTime);
            }
            else if (!_cameraSwitch.playerIsInShootPose && !_cameraSwitch.playerAim)
            {
                Vector3 inputs = new Vector3(horizontal, 0, vertical).normalized;
                float targetAngle = Mathf.Atan2(inputs.x, inputs.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, targetAngle, 0);
                _characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            }

            yield return null;
        }

        _characterControllerLogic.enabled = true;
        _animator.SetBool("ChangePos", false);
        StartCoroutine(CooldownCorutine());
    }

    IEnumerator CooldownCorutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        playerDashing = false;
        ghostFormVFX.SetActive(false);
    }
}
