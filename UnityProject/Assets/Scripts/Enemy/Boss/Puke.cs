using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puke : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _bulletStartPos;
    [SerializeField] private int _bulletPower = 10;
    [SerializeField] private float _bulletSpeed = 5f;
    [SerializeField] private float _bulletFlyHeight = 10f;
    [SerializeField] [Tooltip("Set middle corner in second position of array")] private Vector3[] _arenaThreeCornersPositions;
    [SerializeField] private Transform _arenaCenter;
    [Header("Puke")]
    [SerializeField] private int _numberOfRandomBullets = 5;
    [SerializeField] private int _numberOfToPlayerBullets = 5;
    [SerializeField] private float _timeToSpawnNextBullet = 2f;
    [Header("Mega Puke")]
    [SerializeField] private int _nuberOfMegaPukeBullets = 10;
    [SerializeField] private float _timeToSpawnNextBulletInMegaPuke = 0.2f;

    [HideInInspector] public bool pukeEnd;
    [HideInInspector] public bool megaPukeEnd;

    private BossBullet _bossBullet;

    public void StartPuke()
    {
        StartCoroutine(PukeCourutine());
    }

    public void StartMegaPuke()
    {
        StartCoroutine(MegaPukeCourutine());
    }

    private Vector3 RandomEndBulletPosition()
    {
        float x;
        float z;

        if (_arenaThreeCornersPositions[0].x > _arenaThreeCornersPositions[1].x)
        {
            x = Random.Range(_arenaThreeCornersPositions[1].x, _arenaThreeCornersPositions[0].x);
        }
        else
        {
            x = Random.Range(_arenaThreeCornersPositions[0].x, _arenaThreeCornersPositions[1].x);
        }


        if (_arenaThreeCornersPositions[2].z > _arenaThreeCornersPositions[1].z)
        {
            z = Random.Range(_arenaThreeCornersPositions[1].z, _arenaThreeCornersPositions[2].z);
        }
        else
        {
            z = Random.Range(_arenaThreeCornersPositions[2].z, _arenaThreeCornersPositions[1].z);
        }

        return new Vector3(x, _arenaThreeCornersPositions[1].y, z);
    }

    private void BulletSet()
    {
        var newBullet = Instantiate(_bullet);
        Vector3 newPos = _bulletStartPos.position;
        newBullet.transform.position = newPos;

        _bossBullet = newBullet.GetComponent<BossBullet>();
        _bossBullet.startPosition = _bulletStartPos.position;
        _bossBullet.power = _bulletPower;
        _bossBullet.speed = _bulletSpeed;
        _bossBullet.flyHeight = _bulletFlyHeight;
        _bossBullet.playerData = _playerTransform.GetComponent<PlayerData>();
        _bossBullet.bossMovement = GetComponent<BossMovement>();
    }

    private IEnumerator PukeCourutine()
    {
        int normalBulletsSpawned = 0;
        int randomBulletsSpawned = 0;
        int bulletsToSpawn = _numberOfRandomBullets + _numberOfToPlayerBullets;

        for (int i = 0; i < bulletsToSpawn; i++)
        {
            BulletSet();

            if (normalBulletsSpawned < _numberOfToPlayerBullets && randomBulletsSpawned < _numberOfRandomBullets)
            {
                int random = Random.Range(0, 100);

                if (random <= 49)
                {
                    normalBulletsSpawned++;
                    _bossBullet.endPosition = _playerTransform.position;
                }
                else
                {
                    randomBulletsSpawned++;
                    _bossBullet.endPosition = RandomEndBulletPosition();
                }
            }
            else if (normalBulletsSpawned < _numberOfToPlayerBullets && randomBulletsSpawned == _numberOfRandomBullets)
            {
                normalBulletsSpawned++;
                _bossBullet.endPosition = _playerTransform.position;
            }
            else if (normalBulletsSpawned == _numberOfToPlayerBullets && randomBulletsSpawned < _numberOfRandomBullets)
            {
                randomBulletsSpawned++;
                _bossBullet.endPosition = RandomEndBulletPosition();
            }

            _bossBullet.fly = true;
            yield return new WaitForSeconds(_timeToSpawnNextBullet);
        }

        pukeEnd = true;
    }

    private IEnumerator MegaPukeCourutine()
    {
        for (int i = 0; i < _nuberOfMegaPukeBullets; i++)
        {
            BulletSet();
            _bossBullet.endPosition = RandomEndBulletPosition();
            _bossBullet.fly = true;
            yield return new WaitForSeconds(_timeToSpawnNextBulletInMegaPuke);
        }
        megaPukeEnd = true;
    }
}
