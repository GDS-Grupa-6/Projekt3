using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState { Laser, CannonFire, Normal }

[RequireComponent(typeof(Animator))]
public class BossCombatLogic : MonoBehaviour
{
    [HideInInspector] public BossState bossState;

    private Animator animator;
    private InputManager inputManager;

    private void Awake()
    {
        animator = GetComponent<Animator>();

        inputManager = FindObjectOfType<InputManager>();
        inputManager.inputSystem.Debug.ChangeBossState.performed += _ => ChangeBossState(BossState.Laser);
    }

    public void ChangeBossState(BossState state)
    {
        switch (state)
        {
            case BossState.Laser:
                bossState = BossState.Laser;
                animator.ResetTrigger("Normal");
                animator.SetTrigger("Laser");
                break;
            case BossState.CannonFire:
                bossState = BossState.CannonFire;
                break;
            case BossState.Normal:
                bossState = BossState.Normal;
                animator.SetTrigger("Normal");
                animator.ResetTrigger("Atack");
                break;
            default:
                break;
        }
    }
}
