using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal enum BossState { Laser, CannonFire }

public class BossCombatLogic : MonoBehaviour
{
    internal BossState bossState = BossState.Laser;
}
