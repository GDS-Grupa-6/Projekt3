using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossPhases { empty, First, Second, Third }
public enum BossFirstPhaseStates { empty, StartPhase, ParabolaJump, Wave360, TwoStrikes, Wave45, Locked, SpinAttack, Puke, MegaPuke, Tired }

[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    public Transform centerOfArena;

    /*[HideInInspector]*/
    public BossPhases bossPhase;
    /*[HideInInspector]*/
    public BossFirstPhaseStates bossFirstPhaseState;
    [HideInInspector] public float distanceToPlayer;
    [HideInInspector] public bool findTargetVectorToParabolaJump;

    private Animator _animator;
    private Transform _playerTransform;
    private bool _setAnimatorTrigger;
    private string _animatorTriggerName;

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

        if (_setAnimatorTrigger && _animatorTriggerName != null)
        {
            _setAnimatorTrigger = false;
            _animator.SetTrigger(_animatorTriggerName);
        }
    }

    public void ChangeBossPhase(BossPhases phase)
    {
        bossPhase = phase;

        switch (phase)
        {
            case BossPhases.First:
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
                findTargetVectorToParabolaJump = false;
                _animator.enabled = true;
                break;
            case BossFirstPhaseStates.ParabolaJump:
                break;
            case BossFirstPhaseStates.Wave360:
                findTargetVectorToParabolaJump = true;
                break;
            case BossFirstPhaseStates.TwoStrikes:
                break;
            case BossFirstPhaseStates.Wave45:
                break;
            case BossFirstPhaseStates.Locked:
                break;
            case BossFirstPhaseStates.SpinAttack:
                findTargetVectorToParabolaJump = true;
                break;
            case BossFirstPhaseStates.Puke:
                break;
            case BossFirstPhaseStates.MegaPuke:
                break;
            case BossFirstPhaseStates.Tired:
                break;
            default:
                break;
        }
    }
}
