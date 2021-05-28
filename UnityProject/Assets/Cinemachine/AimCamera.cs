using Cinemachine;
using UnityEngine;

public class AimCamera : CinemachineExtension
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform targetEnemy;
    [Space(10)]
    [SerializeField] private float rotationXOffset;
    [SerializeField] private float rotationYOffset;

    private CameraSwitch cameraSwitch;

    void Awake()
    {
        cameraSwitch = FindObjectOfType<CameraSwitch>();

        base.Awake();
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (vcam.Follow && cameraSwitch.playerAim)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                Vector3 target = new Vector3(targetEnemy.transform.position.x, player.transform.position.y, targetEnemy.transform.position.z);

                state.RawOrientation = Quaternion.Euler(player.eulerAngles.x + rotationYOffset, player.eulerAngles.y + rotationXOffset, 0);
                player.LookAt(target);
            }
        }
    }
}
