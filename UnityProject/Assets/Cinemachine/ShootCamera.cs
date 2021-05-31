using UnityEngine;
using System.Collections;
using Cinemachine;

public class ShootCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraArm;
    [Space(10)]
    [Tooltip("Szybkość myszki horyzontalnie")] public float horizontalSpeed = 10;
    [Tooltip("Szybkość myszki wertykalnie")] [SerializeField] private float verticalSpeed = 10f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [Range(1, 180)] [SerializeField] private float maxClampAngle = 80f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [Range(-180, -1)] [SerializeField] private float minClampAngle = -40;

    private CameraSwitch cameraSwitch;
    private float turnSmoothVelocity;
    private InputManager inputManager;
    private Vector3 startingRotation;

    void Awake()
    {
        inputManager = FindObjectOfType<InputManager>();
        cameraSwitch = FindObjectOfType<CameraSwitch>();
    }

    private void Update()
    {
        if (cameraSwitch.playerIsInShootPose)
        {
            Vector2 deltaInput = inputManager.GetMouseDelta();
            startingRotation.x = deltaInput.x * verticalSpeed * Time.deltaTime;
            startingRotation.y += deltaInput.y * horizontalSpeed * Time.deltaTime;
            startingRotation.y = Mathf.Clamp(startingRotation.y, minClampAngle, maxClampAngle);

            cameraArm.localRotation = Quaternion.Euler(-startingRotation.y, 0, 0);
            player.Rotate(0, startingRotation.x, 0);
        }
    }

    public void ResetXCameraRotation()
    {
        startingRotation.y = 0;
    }
}
