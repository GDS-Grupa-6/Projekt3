using UnityEngine;

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

    private BossCobatStates _currentState;
    private BossMovement _bossMovement;
    private int _numberOfStrikes;
    private Animator _animator;

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
            default:
                break;
        }
    }

    public void EndStrike()
    {
        _numberOfStrikes--;
        _animator.SetInteger("NumberOfStrikes", _numberOfStrikes);
    }

    public void ActivePukeFog()
    {
        _pukeSphere.SetActive(true);
        _pukeFog.SetActive(true);
    }

    public void DesactivePukeFog()
    {
        _pukeSphere.SetActive(false);
        _pukeFog.SetActive(false);
    }
}
