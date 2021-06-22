using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases { empty, First, Second, Third }
public enum BossFirstPhaseStates { empty, StartPhase, Wave360, Puke, PlayerNear, PlayerFar }

[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    [SerializeField] private Transform _centerOfArena;
    public float distancePlayerIsNear = 15f;
    [SerializeField] private Transform[] _arenaEdges;
    [SerializeField] private Collider _weponCollider;

    [HideInInspector] public BossPhases bossPhase;
    [HideInInspector] public BossFirstPhaseStates bossFirstPhaseState;
    [HideInInspector] public Vector3 targetPosition;
    [HideInInspector] public float distanceToPlayer;
    [HideInInspector] public bool startPhase;
    [HideInInspector] public bool jumpToPlayer;
    [HideInInspector] public int animationRepeds;

    private Animator _animator;
    private Transform _playerTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _playerTransform = FindObjectOfType<CharacterController>().gameObject.transform;
        bossPhase = BossPhases.empty;
        bossFirstPhaseState = BossFirstPhaseStates.empty;
        WeponOffCollider();
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        _animator.SetFloat("DistanceToPlayer", distanceToPlayer);
        _animator.SetInteger("Repeds", animationRepeds);
    }

    private bool Random50()
    {
        int random = Random.Range(0, 2);

        if (random == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void AnimationPlayed()
    {
        animationRepeds--;
    }

    public void CheckDistance()
    {
        if (distanceToPlayer > distancePlayerIsNear)
        {
            ChangeFirstPhaseState(BossFirstPhaseStates.PlayerFar);
        }
        else
        {
            ChangeFirstPhaseState(BossFirstPhaseStates.PlayerNear);
        }
    }

    public void WeponOffCollider()
    {
        _weponCollider.enabled = false;
    }

    public void WeponOnController()
    {
        _weponCollider.enabled = true;
    }

    public Vector3 SelectPointAwayPlayer()
    {
        Transform largest = _arenaEdges[0];
        float[] distances = new float[4]
        {
            Vector3.Distance(_arenaEdges[0].position, _playerTransform.position),
            Vector3.Distance(_arenaEdges[1].position, _playerTransform.position),
            Vector3.Distance(_arenaEdges[2].position, _playerTransform.position),
            Vector3.Distance(_arenaEdges[3].position, _playerTransform.position)
        };

        float largestDistans = distances[0];

        for (int i = 0; i < _arenaEdges.Length; i++)
        {
            if (distances[i] > largestDistans)
            {
                largestDistans = distances[i];
                largest = _arenaEdges[i];
            }
        }

        return largest.position;
    }

    public void ChangeBossPhase(BossPhases phase)
    {
        bossPhase = phase;

        switch (phase)
        {
            case BossPhases.First:
                ChangeFirstPhaseState(BossFirstPhaseStates.StartPhase);
                break;
            case BossPhases.Second:
                break;
            case BossPhases.Third:
                break;
            default:
                break;
        }
    }

    public void ChangeFirstPhaseState(BossFirstPhaseStates state)
    {
        bossFirstPhaseState = state;

        switch (state)
        {
            case BossFirstPhaseStates.StartPhase:
                _animator.enabled = true;
                targetPosition = _centerOfArena.position;
                startPhase = true;
                _animator.SetBool("ToPlayer", true);
                _animator.SetBool("JumpEnd", false);
                break;

            case BossFirstPhaseStates.Wave360:
                _animator.SetBool("JumpEnd", false);
                //ustawienie rotacji
                break;

            case BossFirstPhaseStates.Puke:
                _animator.SetBool("JumpEnd", false);
                //ustawienie rotacji
                break;

            case BossFirstPhaseStates.PlayerNear:
                _animator.SetBool("PlayerNear", true);
                break;

            case BossFirstPhaseStates.PlayerFar:
                _animator.SetBool("PlayerNear", false);
                jumpToPlayer = true /*Random50()*/;
                if (jumpToPlayer)
                {
                    targetPosition = _playerTransform.position;
                }
                else
                {
                    targetPosition = SelectPointAwayPlayer();
                }
                break;
            default:
                break;
        }
    }
}
