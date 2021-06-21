using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(BossMovement))]
public class BossCombatLaser : MonoBehaviour
{
    [SerializeField] private Transform _startLaserPos;
    [SerializeField] [Range(0, 100)] private float _bossRotateSpeed = 10;
    [SerializeField] private Transform[] _laserBossPositions;
    public int maxNumberOfSpin = 4;
    [SerializeField] private float _changeModeTime = 5;
    [SerializeField] private float _laserPower = 10;
    [SerializeField] private float _laserHeal = 10;
    [Space(10)]
    [SerializeField] Material _normalLaserMaretial;
    [SerializeField] Material _ghostLaserMaterial;

    [HideInInspector] public int spinNumber;
    [HideInInspector] public Transform targetLaserJump;

    private LineRenderer _lineRenderer;
    private BossMovement _bossMovement;
    private bool _spinToLeftSide;
    private Vector3 _bossRoatation;
    private bool _isGhostMode;
    private bool _laserHasHit;
    private RaycastHit _hit;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _bossMovement = GetComponent<BossMovement>();
        _lineRenderer.material = _normalLaserMaretial;
    }

    private void Update()
    {
        if (Physics.Raycast(_startLaserPos.position, _startLaserPos.forward, out _hit))
        {
            _laserHasHit = true;
        }
        else
        {
            _laserHasHit = false;
        }
    }

    private void LaserMode()
    {
        if (_isGhostMode)
        {
            _isGhostMode = false;
            _lineRenderer.material = _normalLaserMaretial;
        }
        else
        {
            _isGhostMode = true;
            _lineRenderer.material = _ghostLaserMaterial;
        }
    }

    public IEnumerator ChangeLaserModeCourutine()
    {
        yield return new WaitForSeconds(_changeModeTime);
        LaserMode();
    }

    public void CreateLaser()
    {
        _lineRenderer.SetPosition(0, _startLaserPos.position);

        if (_laserHasHit)
        {
            if (_hit.collider)
            {
                _lineRenderer.SetPosition(1, _hit.point);
            }

            if (_hit.collider.tag == "Player")
            {
                Dash dash = _hit.collider.GetComponent<Dash>();
                PlayerData playerData = _hit.collider.GetComponent<PlayerData>();

                if (_isGhostMode && dash.playerDashing && playerData.currentHealth != playerData.maxHealth)
                {
                    playerData.Heal(_laserHeal);
                }
                else
                {
                    playerData.TakeDamage(_laserPower);
                }
            }
        }
        else
        {
            _lineRenderer.SetPosition(1, _startLaserPos.forward * 50000);
        }
    }

    public void DestroyLaser()
    {
        _lineRenderer.SetPosition(0, Vector3.zero);
        _lineRenderer.SetPosition(1, Vector3.zero);
        StopAllCoroutines();
    }

    public void SpinBoss()
    {
        float maxRotationDelta = targetLaserJump.eulerAngles.y + 89.99f;
        float minRotationDelta = targetLaserJump.eulerAngles.y;

        if (_spinToLeftSide)
        {
            if (transform.eulerAngles.y == minRotationDelta)
            {
                _spinToLeftSide = false;
                spinNumber++;
            }

            _bossRoatation.x += -_bossRotateSpeed * Time.deltaTime;
        }
        else
        {
            if (transform.eulerAngles.y == maxRotationDelta)
            {
                _spinToLeftSide = true;
            }

            _bossRoatation.x += _bossRotateSpeed * Time.deltaTime;
        }

        _bossRoatation.x = Mathf.Clamp(_bossRoatation.x, minRotationDelta, maxRotationDelta);
        transform.rotation = Quaternion.Euler(0, _bossRoatation.x, 0);
    }

    public void SelectBossLaserPosition()
    {
        Transform largest = _laserBossPositions[0];
        float[] distances = new float[4]
        {
            Vector3.Distance(_laserBossPositions[0].position, _bossMovement.player.position),
            Vector3.Distance(_laserBossPositions[1].position, _bossMovement.player.position),
            Vector3.Distance(_laserBossPositions[2].position, _bossMovement.player.position),
            Vector3.Distance(_laserBossPositions[3].position, _bossMovement.player.position)
        };

        float largestDistans = distances[0];

        for (int i = 0; i < _laserBossPositions.Length; i++)
        {
            if (distances[i] > largestDistans)
            {
                largestDistans = distances[i];
                largest = _laserBossPositions[i];
            }
        }

        targetLaserJump = largest;
    }
}
