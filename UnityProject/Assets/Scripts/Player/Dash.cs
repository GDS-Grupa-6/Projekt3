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
    private CharacterController characterController;
    private InputManager inputManager;
    private Animator animator;
    private CharacterControllerLogic characterControllerLogic;
    private CameraSwitch cameraSwitch;

    void Start()
    {
        characterControllerLogic = GetComponent<CharacterControllerLogic>();
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        inputManager = FindObjectOfType<InputManager>();
        cameraSwitch = FindObjectOfType<CameraSwitch>();
        ghostFormVFX.SetActive(false);
    }

    void Update()
    {
        if (inputManager.PlayerDash() && !playerDashing)
        {
            characterControllerLogic.enabled = false;
            playerDashing = true;
            StartCoroutine(DashCourutine());
        }
    }

    IEnumerator DashCourutine()
    {
        float startTime = Time.time;
        ghostFormVFX.SetActive(true);

        while (Time.time < startTime + dashTime)
        {
            float horizontal = inputManager.MovementControls().x;
            float vertical = inputManager.MovementControls().y;

            if (cameraSwitch.playerIsInShootPose || cameraSwitch.playerAim)
            {
                Vector3 move = transform.right * horizontal + transform.forward * vertical;
                characterController.Move(move * dashSpeed * Time.deltaTime);
            }
            else if (!cameraSwitch.playerIsInShootPose && !cameraSwitch.playerAim)
            {
                Vector3 inputs = new Vector3(horizontal, 0, vertical).normalized;
                float targetAngle = Mathf.Atan2(inputs.x, inputs.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
                transform.rotation = Quaternion.Euler(0, targetAngle, 0);
                characterController.Move(transform.forward * dashSpeed * Time.deltaTime);
            }

            yield return null;
        }

        characterControllerLogic.enabled = true;
        StartCoroutine(CooldownCorutine());
    }

    IEnumerator CooldownCorutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        playerDashing = false;
        ghostFormVFX.SetActive(false);
    }
}
