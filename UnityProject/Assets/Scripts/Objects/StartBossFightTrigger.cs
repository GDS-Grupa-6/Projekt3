using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartBossFightTrigger : MonoBehaviour
{
    [SerializeField] private BossCombatLogic _combatLogic;
    [SerializeField] private GameObject _bossHUD;

    private void Awake()
    {
        _bossHUD.SetActive(false);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _bossHUD.SetActive(true);
            _combatLogic.ChangeBossPhase(BossPhases.First);
        }
    }
}
