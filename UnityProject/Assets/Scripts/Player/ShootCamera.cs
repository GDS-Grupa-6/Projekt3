using UnityEngine;
using Cinemachine;

public class ShootCamera : CinemachineExtension
{
    [SerializeField] private Transform player;
    [Space(10)]
    [Tooltip("Wygładzenie obrotu kamery")] [SerializeField] private float turnSmoothTime = 0.1f;
    [Tooltip("Szybkość myszki horyzontalnie")] [SerializeField] private float horizontalSpeed = 10;
    [Tooltip("Szybkość myszki wertykalnie")] [SerializeField] private float verticalSpeed = 10f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [SerializeField] private float clampAngle = 80f;

    private float turnSmoothVelocity;
    private InputManager inputManager;
    private Vector3 startingRotation;


    void Awake()
    {
        inputManager = InputManager.Instance;

        if (startingRotation == null)
        {
            startingRotation = transform.localRotation.eulerAngles;
        }

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {  
        if (vcam.Follow && player.GetComponent<Movement>().playerIsShooting)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

               state.RawOrientation = Quaternion.Euler(-startingRotation.y, player.eulerAngles.y, 0);
            }   
        }
    }
}
