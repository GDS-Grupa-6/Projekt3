using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//do zmiany
public enum BossState { Laser, CannonFire, Normal }

[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    public BossState bossState;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }
}
