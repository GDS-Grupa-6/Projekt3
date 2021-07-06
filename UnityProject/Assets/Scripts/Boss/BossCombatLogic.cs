using UnityEngine;
using System.Collections;

public enum BossCobatStates { empty, StartFight, Strikes, Locked, JumpToPlayer, Puke }

[RequireComponent(typeof(BossMovement))]
[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    [Header("Waves options")]
    public Wave wave360;
    public Wave wave45;
    [Header("Strike options")]
    [SerializeField] private float _attackRange;
    [SerializeField] private int _maxNumberOfStrikes = 5;
    [Header("\"Player is far\" options")]
    [SerializeField] private float _playerIsFarDistance;
    [SerializeField] [Range(0, 100)] private int _chanceToPuke;
    [Header("Puke options")]
    [SerializeField] private GameObject _pukeSphere;
    [SerializeField] private GameObject _pukeFog;
    [SerializeField] private Transform _pukeBossPos;
    [SerializeField] private GameObject _bulletPrefab;
    [Space(10)]
    [SerializeField] private int _numberOfAllBulletsInNormalPuke = 10;
    [SerializeField] private int _numberOfRandomBulletsInNormalPuke = 5;
    [SerializeField] private float _timeToSpawnNextBulletInNormalPuke = 1f;
    [Space(10)]
    [SerializeField] private int _numberOfBulletsInMegaPuke = 10;
    [SerializeField] private float _timeToSpawnNextBulletInMegaPuke = 0.2f;
    [Space(10)]
    [SerializeField] private Transform _bulletsStartPoint;
    [SerializeField] private Transform[] _bulletsRandomPoints;

    private BossCobatStates _currentState;
    private BossMovement _bossMovement;
    private int _numberOfStrikes;
    private Animator _animator;
    private bool _pukeActive;
    private int _numberOffAllBullets;

    [HideInInspector] public int destroyedBullets;

    private void Awake()
    {
        _pukeSphere.SetActive(false);
        _pukeFog.SetActive(false);
        _animator = GetComponent<Animator>();
        _bossMovement = GetComponent<BossMovement>();
        _currentState = BossCobatStates.empty;
    }

    private void Update()
    {
        if (_currentState == BossCobatStates.Strikes)
        {
            CheckStrikesDistance();
        }
    }

    private void CheckStrikesDistance()
    {
        if (_bossMovement.DistanceToPlayer() > _attackRange)
        {
            _bossMovement.moveBoss = true;
            _animator.SetBool("MoveBoss", _bossMovement.moveBoss);
        }
        else if (_bossMovement.DistanceToPlayer() <= _attackRange)
        {
            _bossMovement.moveBoss = false;
            _animator.SetBool("MoveBoss", _bossMovement.moveBoss);
        }
    }

    private IEnumerator PukeCourutine()
    {
        _numberOffAllBullets = _numberOfBulletsInMegaPuke + _numberOfAllBulletsInNormalPuke;
        int createdRandomBullets = 0;

        for (int i = 0; i < _numberOfAllBulletsInNormalPuke; i++)
        {
            if (createdRandomBullets + 1 <= _numberOfRandomBulletsInNormalPuke)
            {
                int random = Random.Range(0, 2);

                if (random == 0)
                {
                    createdRandomBullets++;
                    CreateBullet(true);
                }
                else
                {
                    CreateBullet(false);
                }
            }
            else
            {
                CreateBullet(false);
            }

            yield return new WaitForSeconds(_timeToSpawnNextBulletInNormalPuke);
        }

        while (destroyedBullets < _numberOfAllBulletsInNormalPuke)
        {
            yield return new WaitForFixedUpdate();
        }

        for (int i = 0; i < _numberOfBulletsInMegaPuke; i++)
        {
            CreateBullet(true);
            yield return new WaitForSeconds(_timeToSpawnNextBulletInMegaPuke);
        }

        while (destroyedBullets < _numberOffAllBullets)
        {
            yield return new WaitForFixedUpdate();
        }

        _animator.SetTrigger("Tired");
        destroyedBullets = 0;
        _pukeActive = false;
    }

    private void CreateBullet(bool isRandom)
    {
        var obj = Instantiate(_bulletPrefab);
        obj.transform.position = _bulletsStartPoint.localPosition;
        BossBullet bossBullet = obj.GetComponent<BossBullet>();
        bossBullet.startPos = _bulletsStartPoint.position;
        bossBullet.combatLogic = this;

        if (isRandom)
        {
            bossBullet.target = _bulletsRandomPoints[Random.Range(0, _bulletsRandomPoints.Length)].localPosition;
        }
        else
        {
            bossBullet.target = _bossMovement.player.transform.position;
        }

        bossBullet.fly = true;
    }

    private void ActivePukeFog()
    {
        _pukeSphere.SetActive(true);
        _pukeFog.SetActive(true);
    }

    public void CheckDistanceForStates()
    {
        if (_bossMovement.DistanceToPlayer() >= _playerIsFarDistance)
        {
            int random = Random.Range(0, 100);

            if (random <= _chanceToPuke)
            {
                SetCombatState(BossCobatStates.Puke);
            }
            else
            {
                SetCombatState(BossCobatStates.JumpToPlayer);
            }
        }
        else if (_bossMovement.DistanceToPlayer() < _playerIsFarDistance)
        {
            SetCombatState(BossCobatStates.Strikes);
        }
    }

    public void SetCombatState(BossCobatStates bossCobatStates)
    {
        _currentState = bossCobatStates;

        switch (bossCobatStates)
        {
            case BossCobatStates.StartFight:
                _bossMovement.bossJump = true;
                break;
            case BossCobatStates.Strikes:
                _numberOfStrikes = Random.Range(1, _maxNumberOfStrikes);
                _animator.SetInteger("NumberOfStrikes", _numberOfStrikes);
                _animator.SetTrigger("Strike");
                break;
            case BossCobatStates.Locked:
                _bossMovement.moveBoss = false;
                _animator.SetBool("MoveBoss", _bossMovement.moveBoss);
                break;
            case BossCobatStates.JumpToPlayer:
                _bossMovement.bossMoveTarget = _bossMovement.player.transform.position;
                _animator.SetTrigger("JumpToPlayer");
                _bossMovement.bossJump = true;
                break;
            case BossCobatStates.Puke:
                _bossMovement.bossMoveTarget = _pukeBossPos.localPosition;
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                _animator.SetTrigger("Puke");
                _bossMovement.bossJump = true;
                break;
            default:
                break;
        }
    }

    public void EndStrike()
    {
        _numberOfStrikes--;
        _animator.SetInteger("NumberOfStrikes", _numberOfStrikes);
    }

    public void DesactivePukeFog()
    {
        _pukeSphere.SetActive(false);
        _pukeFog.SetActive(false);
    }

    public void StartPuke()
    {
        if (!_pukeActive)
        {
            _pukeActive = true;
            ActivePukeFog();
            StartCoroutine(PukeCourutine());
        }
    }
}
