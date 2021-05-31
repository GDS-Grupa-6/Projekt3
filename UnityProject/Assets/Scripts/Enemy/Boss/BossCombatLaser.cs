using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BossMovement))]
public class BossCombatLaser : MonoBehaviour
{
    [SerializeField] private Transform startLaserPos;
    [SerializeField] [Range(0, 3)] private float bossRotateSpeed = 1;
    [SerializeField] private Transform[] laserBossPositions;

    private LineRenderer lineRenderer;
    private BossMovement bossMovement;
    private bool spinToLeftSide;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        bossMovement = GetComponent<BossMovement>();
    }

    public void CreateLaser()
    {
        lineRenderer.SetPosition(0, startLaserPos.position);

        RaycastHit hit;
        if (Physics.Raycast(startLaserPos.position, startLaserPos.forward, out hit))
        {
            if (hit.collider)
            {
                lineRenderer.SetPosition(1, hit.point);
            }
        }
        else
        {
            lineRenderer.SetPosition(1, startLaserPos.forward * 50000);
        }
    }

    public void SpinBoss()
    {
        if (spinToLeftSide)
        {
            if (transform.eulerAngles.y <= bossMovement.bossTargetPosition.eulerAngles.y)
            {
                spinToLeftSide = false;
            }

            transform.Rotate(Vector3.up * -bossRotateSpeed);
        }
        else
        {
            if (transform.eulerAngles.y >= bossMovement.bossTargetPosition.eulerAngles.y + 90)
            {
                spinToLeftSide = true;
            }

            transform.Rotate(Vector3.up * bossRotateSpeed);
        }
    }

    public void SelectBossLaserPosition()
    {
        Transform largest = laserBossPositions[0];
        float[] distances = new float[4]
        {
            Vector3.Distance(laserBossPositions[0].position, bossMovement.player.position),
            Vector3.Distance(laserBossPositions[1].position, bossMovement.player.position),
            Vector3.Distance(laserBossPositions[2].position, bossMovement.player.position),
            Vector3.Distance(laserBossPositions[3].position, bossMovement.player.position)
        };

        float largestDistans = distances[0];

        for (int i = 0; i < laserBossPositions.Length; i++)
        {
            if (distances[i] > largestDistans)
            {
                largestDistans = distances[i];
                largest = laserBossPositions[i];
            }
        }

        bossMovement.bossTargetPosition = largest;
    }
}
