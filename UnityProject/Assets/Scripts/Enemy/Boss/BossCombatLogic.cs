using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases { empty, First, Second, Third }
public enum BossFirstPhaseStates { empty, StartPhase, Wave360 }

[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    public Transform centerOfArena;

    [HideInInspector] public BossPhases bossPhase;
    [HideInInspector] public BossFirstPhaseStates bossFirstPhaseState;


    [HideInInspector] public Transform targetTransform;

    private float distanceToPlayer;
    private Animator _animator;
    private Transform _playerTransform;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.enabled = false;
        _playerTransform = FindObjectOfType<CharacterController>().gameObject.transform;
        bossPhase = BossPhases.empty;
        bossFirstPhaseState = BossFirstPhaseStates.empty;
    }

    private void Update()
    {
        distanceToPlayer = Vector3.Distance(transform.position, _playerTransform.position);
        _animator.SetFloat("DistanceToPlayer", distanceToPlayer);

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
                targetTransform = centerOfArena;
                _animator.SetBool("ToPlayer", true);
                _animator.SetBool("JumpEnd", false);
                break;
            case BossFirstPhaseStates.Wave360:
                _animator.SetBool("JumpEnd", false);
                targetTransform = _playerTransform;
                //ustawienie rotacji
                break;
            default:
                break;
        }
    }
}
