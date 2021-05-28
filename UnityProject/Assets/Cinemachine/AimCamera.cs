using Cinemachine;
using UnityEngine;

public class AimCamera : CinemachineExtension
{
    [SerializeField] private Transform player;
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
        if (vcam.Follow && cameraSwitch.playerAim && !cameraSwitch.playerIsInShootPose)
        {
            if (stage == CinemachineCore.Stage.Aim)
            {
                if (cameraSwitch.targetEnemy != null)
                {
                    Vector3 target = new Vector3(cameraSwitch.targetEnemy.transform.position.x, player.transform.position.y, cameraSwitch.targetEnemy.transform.position.z);
                    player.LookAt(target);
                }

                state.RawOrientation = Quaternion.Euler(player.eulerAngles.x + rotationYOffset, player.eulerAngles.y + rotationXOffset, 0);
            }
        }
    }
}
