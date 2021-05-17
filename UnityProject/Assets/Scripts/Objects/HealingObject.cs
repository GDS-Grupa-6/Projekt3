using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour
{
    [SerializeField] private float healValue = 10;

    private PlayerData playerData;
    private Dash dash;

    private void Start()
    {
        playerData = FindObjectOfType<PlayerData>();
        dash = FindObjectOfType<Dash>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dash.playerDashing && playerData.currentHealth != playerData.maxHealth)
            {
                playerData.Heal(healValue);
                Destroy(this.gameObject);
            }
        }
    }
}
