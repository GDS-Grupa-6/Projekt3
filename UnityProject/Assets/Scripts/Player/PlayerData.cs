using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Dash))]
public class PlayerData : MonoBehaviour
{
    public float maxHealth = 100;

    [HideInInspector] public float currentHealth;
    private Dash dash;

    private void Start()
    {
        dash = GetComponent<Dash>();
        SetHealth();
    }

    public void TakeDamage(float damageValue)
    {
        if (!dash.playerDashing)
        {
            if (currentHealth - damageValue < 0)
            {
                currentHealth = 0;
                Debug.Log("<color=red>Gracz umarł</color>");
            }
            else
            {
                currentHealth -= damageValue;
                Debug.Log($"<color=red>Gracz otrzymał: {damageValue} obrażeń i ma teraz {currentHealth} życia</color>");
            }
        }
    }

    public void Heal(float healValue)
    {
        if (currentHealth + healValue > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else
        {
            currentHealth += healValue;
        }

        Debug.Log($"<color=green>Gracz wleczył się o: {healValue} i ma teraz {currentHealth} życia</color>");
    }

    private void SetHealth()
    {
        currentHealth = maxHealth;
    }
}
