using UnityEngine;
using Cinemachine;

public class ShootCamera : CinemachineExtension
{
    [Tooltip("Wygładzenie obrotu kamery")] [SerializeField] private float turnSmoothTime = 0.1f;
    [SerializeField] private Transform player;
    [SerializeField] private float horizontalSpeed = 10;
    [SerializeField] private float verticalSpeed = 10f;
    [SerializeField] private float clampAngle = 80f;

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
        if (vcam.Follow)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector2 deltaInput = inputManager.GetMouseDelta();
                startingRotation.x += deltaInput.x * verticalSpeed * Time.deltaTime;
                startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
                startingRotation.y = Mathf.Clamp(startingRotation.y, -clampAngle, clampAngle);

                float angle = Mathf.SmoothDampAngle(player.eulerAngles.y, startingRotation.x, ref turnSmoothVelocity, turnSmoothTime);
                state.RawOrientation = Quaternion.Euler(-startingRotation.y, angle, 0);

                if (player.GetComponent<Movement>().playerIsShooting)
                {
                    player.rotation = Quaternion.Euler(0, startingRotation.x, 0);
                }
            }
        }
    }
}
