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
    [SerializeField] private float changeModeTime = 5;
    [SerializeField] private float laserPower = 10;
    [SerializeField] private float laserHeal = 10;
    [Space(10)]
    [SerializeField] Material normalLaserMaretial;
    [SerializeField] Material ghostLaserMaterial;

    [HideInInspector] public int spinNumber;
    private LineRenderer lineRenderer;
    private BossMovement bossMovement;
    private bool spinToLeftSide;
    private Vector3 bossRoatation;
    private bool isGhostMode;

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
        bossMovement = GetComponent<BossMovement>();
        lineRenderer.material = normalLaserMaretial;
    }

    private void LaserMode()
    {
        if (isGhostMode)
        {
            isGhostMode = false;
            lineRenderer.material = normalLaserMaretial;
        }
        else
        {
            isGhostMode = true;
            lineRenderer.material = ghostLaserMaterial;
        }
    }

    public IEnumerator ChangeLaserModeCourutine()
    {
        yield return new WaitForSeconds(changeModeTime);
        LaserMode();
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

            if (hit.collider.tag == "Player")
            {
                Dash dash = hit.collider.GetComponent<Dash>();
                PlayerData playerData = hit.collider.GetComponent<PlayerData>();

                if (isGhostMode && dash.playerDashing && playerData.currentHealth != playerData.maxHealth)
                {
                    playerData.Heal(laserHeal);
                }
                else
                {
                    playerData.TakeDamage(laserPower);
                }
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
        StopAllCoroutines();
    }

    public void SpinBoss()
    {
        float maxRotationDelta = bossMovement.bossTargetTransform.eulerAngles.y + 89.99f;
        float minRotationDelta = bossMovement.bossTargetTransform.eulerAngles.y;

        if (spinToLeftSide)
        {
            if (transform.eulerAngles.y == minRotationDelta)
            {
                spinToLeftSide = false;
                spinNumber++;
            }

            bossRoatation.x += -bossRotateSpeed * Time.deltaTime;
        }
        else
        {
            if (transform.eulerAngles.y == maxRotationDelta)
            {
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

        bossMovement.bossTargetTransform = largest;
    }
}
