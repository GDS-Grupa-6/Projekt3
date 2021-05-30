using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class CharacterControllerLogic : MonoBehaviour
{
    [SerializeField] private float directionDampTime = 0.25f;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private float directionSpeed = 3.0f;
    [SerializeField] private float rotationDegreePerSecond = 120f;
    [SerializeField] private float speedDampTime = 0.05f;
    [SerializeField] private float angleDampTime = 0.05f;

    [HideInInspector] public Animator animator;
    private float horizontal = 0.0f;
    private float vertical = 0.0f;
    private AnimatorStateInfo stateInfo;
    private float speed = 0.0f;
    private float direction = 0f;
    private float charAngle = 0f;
    private float sprintValue;

    private int m_LocomotionId = 0;
    private int m_LocomotionPivotLId = 0;
    private int m_LocomotionPivotRId = 0;
    private int hFloat;                                   // Animator variable related to Horizontal Axis.
    private int vFloat;                                   // Animator variable related to Vertical Axis.

    private float LocomotionThreshold { get { return 0.2f; } }
    private CameraSwitch cameraSwitch;
    private InputManager inputManager;


    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;//przenieœ do gamecontrollera

        cameraSwitch = FindObjectOfType<CameraSwitch>();
        inputManager = FindObjectOfType<InputManager>();

        animator = GetComponent<Animator>();

        if (animator.layerCount >= 2)
        {
            animator.SetLayerWeight(1, 1);
        }

        m_LocomotionId = Animator.StringToHash("Base Layer.Locomotion");
        m_LocomotionPivotLId = Animator.StringToHash("Base Layer.LocomotionPivotL");
        m_LocomotionPivotRId = Animator.StringToHash("Base Layer.LocomotionPivotR");

        // set variable for animator 
        hFloat = Animator.StringToHash("inputHorizontal");
        vFloat = Animator.StringToHash("inputVertical");

        inputManager.inputSystem.Player.Sprint.performed += _ => sprintValue = 1;
        inputManager.inputSystem.Player.Sprint.canceled += _ => sprintValue = 0;
    }

    void Update()
    {
        // read horizontal and vertical input from keyboard
        horizontal = inputManager.MovementControls().x;
        vertical = inputManager.MovementControls().y;

        // get horizontal and vertical inputs for animator
        animator.SetFloat(hFloat, horizontal, 0.25f, Time.deltaTime);
        animator.SetFloat(vFloat, vertical, 0.25f, Time.deltaTime);

        stateInfo = animator.GetCurrentAnimatorStateInfo(0);

        charAngle = 0f;
        direction = 0f;

        StickToWorldspace(this.transform, mainCamera.transform, ref direction, ref speed, ref charAngle, IsInPivot());

        if (inputManager.PlayerJumpedThisFrame() && !animator.GetBool("ShootPos"))
        {
            animator.SetTrigger("Jump");
        }

        animator.SetFloat("Speed", speed, speedDampTime, Time.deltaTime);
        animator.SetFloat("Direction", direction, directionDampTime, Time.deltaTime);

        if (speed > LocomotionThreshold)
        {
            if (!IsInPivot())
            {
                animator.SetFloat("Angle", charAngle);
            }
        }
        if (speed < LocomotionThreshold && Mathf.Abs(horizontal) < 0.05f)
        {
            animator.SetFloat("Direction", 0f);
            animator.SetFloat("Angle", 0f);
        }
    }

    private bool IsInPivot()
    {
        return stateInfo.fullPathHash == m_LocomotionPivotLId || stateInfo.fullPathHash == m_LocomotionPivotRId;
    }

    private bool IsInLocomotion()
    {
        return stateInfo.fullPathHash == m_LocomotionId;
    }

    private void StickToWorldspace(Transform root, Transform camera, ref float directionOut, ref float speedOut, ref float angleOut, bool isPivoting)
    {
        Vector3 rootDirection = root.forward;

        Vector3 stickDirection = new Vector3(horizontal, 0, vertical);

        if (inputManager.MovementControls().magnitude > 0)
        {
            speedOut = stickDirection.sqrMagnitude + sprintValue;
        }
        else
        {
            speedOut = 0;
        }

        Vector3 CameraDirection = camera.forward;
        CameraDirection.y = 0.0f;

        Quaternion referentialShift;
        if (cameraSwitch.playerIsInShootPose || cameraSwitch.playerAim)
        {
            referentialShift = transform.rotation;
        }
        else
        {
            referentialShift = Quaternion.FromToRotation(Vector3.forward, CameraDirection);
        }


        Vector3 moveDirection = referentialShift * stickDirection;
        Vector3 axisSign = Vector3.Cross(moveDirection, rootDirection);

        float angleRootToMove = Vector3.Angle(rootDirection, moveDirection) * (axisSign.y >= 0 ? -1f : 1f);

        if (!isPivoting)
        {
            angleOut = angleRootToMove;
        }

        angleRootToMove /= 180f;

        directionOut = angleRootToMove * directionSpeed;
    }
}
