using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BossCombatLaser : BossCombatLogic
{
    [SerializeField] private Transform startLaserPos;
    [SerializeField] [Range(0, 3)] private float bossRotateSpeed = 1;

    private LineRenderer lineRenderer;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        if (bossState == BossState.Laser)
        {
            CreateLaser();
            SpinBoss(bossRotateSpeed);
        }
    }

    private void CreateLaser()
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

    private void SpinBoss(float speed)
    {
        transform.Rotate(Vector3.up * speed);
    }
}
