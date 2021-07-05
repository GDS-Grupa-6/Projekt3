using UnityEngine;

public enum BossCobatStates { empty, StartFight, Strikes, Locked }

[RequireComponent(typeof(BossMovement))]
[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    [Header("Waves options")]
    public Wave wave360;
    public Wave wave45;
    [Header("Strike options")]
    [SerializeField] private float _minDistanceToFollowPlayer;
    [SerializeField] private float _attackRange;
    [SerializeField] private int _maxNumberOfStrikes = 5;

    private BossCobatStates _currentState;
    private BossMovement _bossMovement;
    private int _numberOfStrikes;
    private Animator _animator;

    private void Awake()
    {
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
        if (_bossMovement.DistanceToPlayer() > _minDistanceToFollowPlayer)
        {
            //stany poza zasiêgiem
        }
        else if(_bossMovement.DistanceToPlayer() <= _minDistanceToFollowPlayer)
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
            default:
                break;
        }
    }

    public void EndStrike()
    {
        _numberOfStrikes--;
        _animator.SetInteger("NumberOfStrikes", _numberOfStrikes);
    }
}
