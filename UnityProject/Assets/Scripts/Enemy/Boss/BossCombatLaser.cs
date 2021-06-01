using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BossMovement))]
public class BossCombatLaser : MonoBehaviour
{
    [SerializeField] private Transform startLaserPos;
    [SerializeField] [Range(0, 100)] private float bossRotateSpeed = 10;
    [SerializeField] private Transform[] laserBossPositions;

    public int maxNumberOfSpin = 4;

    /* [HideInInspector]*/
    public int spinNumber;
    private LineRenderer lineRenderer;
    private BossMovement bossMovement;
    private bool spinToLeftSide;
    private Vector3 bossRoatation;

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

    public void DestroyLaser()
    {
        lineRenderer.SetPosition(0, Vector3.zero);
        lineRenderer.SetPosition(1, Vector3.zero);
    }

    public void SpinBoss()
    {
        float maxRotationDelta = bossMovement.bossTargetPosition.eulerAngles.y + 90;
        float minRotationDelta = bossMovement.bossTargetPosition.eulerAngles.y;

        Debug.Log($"Min: {minRotationDelta} Max: {maxRotationDelta}");

        if (spinToLeftSide)
        {
            if (transform.eulerAngles.y == minRotationDelta)
            {
                Debug.Log(transform.eulerAngles);
                spinToLeftSide = false;
                spinNumber++;
            }

            bossRoatation.x += -bossRotateSpeed * Time.deltaTime;
        }
        else
        {
            if (transform.eulerAngles.y == maxRotationDelta)
            {
                Debug.Log(transform.eulerAngles + "1");
                spinToLeftSide = true;
            }

            bossRoatation.x += bossRotateSpeed * Time.deltaTime;
        }

        bossRoatation.x = Mathf.Clamp(bossRoatation.x, minRotationDelta, maxRotationDelta);
        transform.rotation = Quaternion.Euler(0, bossRoatation.x, 0);
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
