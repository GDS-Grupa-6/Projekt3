using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFightTrigger : MonoBehaviour
{
    [SerializeField] private BossCombatLogic _combatLogic;

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _combatLogic.ChangeBossPhase(BossPhases.First);
        }
    }
}
