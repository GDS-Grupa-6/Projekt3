using UnityEngine;
using System.Collections;
using Cinemachine;

public class ShootCamera : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform cameraArm;
    [Space(10)]
    [Tooltip("Szybkość myszki horyzontalnie")] public float _horizontalSpeed = 10;
    [Tooltip("Szybkość myszki wertykalnie")] [SerializeField] private float _verticalSpeed = 10f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [Range(1, 180)] [SerializeField] private float _maxClampAngle = 80f;
    [Tooltip("Zablokowanie myszki wertykalnie")] [Range(-180, -1)] [SerializeField] private float _minClampAngle = -40;

    private CameraSwitch _cameraSwitch;
    private float _turnSmoothVelocity;
    private InputManager _inputManager;
    private Vector3 _startingRotation;

    void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
        _cameraSwitch = FindObjectOfType<CameraSwitch>();
    }

    private void Update()
    {
        if (_cameraSwitch.playerIsInShootPose)
        {
            Vector2 deltaInput = _inputManager.GetMouseDelta();
            _startingRotation.x = deltaInput.x * _verticalSpeed * Time.deltaTime;
            _startingRotation.y += deltaInput.y * _horizontalSpeed * Time.deltaTime;
            _startingRotation.y = Mathf.Clamp(_startingRotation.y, _minClampAngle, _maxClampAngle);

            cameraArm.localRotation = Quaternion.Euler(-_startingRotation.y, 0, 0);
            player.Rotate(0, _startingRotation.x, 0);
        }
    }

    public void ResetXCameraRotation()
    {
        _startingRotation.y = 0;
    }
}
