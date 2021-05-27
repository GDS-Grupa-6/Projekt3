using UnityEngine;
using Cinemachine;

public class ShootCamera : CinemachineExtension
{
    [SerializeField] private Transform player;
    [Space(10)]
    [Tooltip("Szybkość myszki horyzontalnie")] public float horizontalSpeed = 10;
    [Tooltip("Szybkość myszki wertykalnie")] [SerializeField] private float verticalSpeed = 10f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [Range(1, 180)] [SerializeField] private float maxClampAngle = 80f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [Range(-180, -1)] [SerializeField] private float minClampAngle = -40;
    [SerializeField] private CameraSwitch cameraSwitch;

    private float turnSmoothVelocity;
    private InputManager inputManager;
    private Vector3 startingRotation;


    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();

        if (startingRotation == null)
        {
            startingRotation = transform.localRotation.eulerAngles;
        }

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow && cameraSwitch.playerIsInShootPose)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, minClampAngle, maxClampAngle);

                state.RawOrientation = Quaternion.Euler(-startingRotation.y, player.eulerAngles.y, 0);
            }
        }
    }
}
