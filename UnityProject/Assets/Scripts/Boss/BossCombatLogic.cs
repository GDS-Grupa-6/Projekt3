using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossCobatStates { empty, StartFight }

[RequireComponent(typeof(BossMovement))]
public class BossCombatLogic : MonoBehaviour
{
    public Wave wave360;
    public Wave wave45;

    private BossCobatStates _cobatStates;
    private BossMovement _bossMovement;

    private void Awake()
    {
        _bossMovement = GetComponent<BossMovement>();
        _cobatStates = BossCobatStates.empty;
    }

    public void SetCombatState(BossCobatStates bossCobatStates)
    {
        switch (bossCobatStates)
        {
            case BossCobatStates.StartFight:
                _bossMovement._bossJump = true;
                break;
            default:
                break;
        }
    }
}
