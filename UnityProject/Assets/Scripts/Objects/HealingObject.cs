using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingObject : MonoBehaviour
{
    [Header("Healing options")]
    [SerializeField] private float healValue = 10f;
    [SerializeField] private bool destroyAfterDash;
    [Header("Damage options")]
    [SerializeField] private bool thisObjectCanHitPlayer;
    [SerializeField] private float damageValue = 10f;
    [SerializeField] private float bounceForce = 20f;
    [SerializeField] private float bounceDistance = 4f;
    [SerializeField] private float hitEffectTime = 2f;

    private PlayerData playerData;
    private Dash dash;
    private Movement movement;
    private Vector3 targetPos;
    private CharacterController characterController;
    private bool hit;

    private void Start()
    {
        characterController = FindObjectOfType<CharacterController>();
        movement = FindObjectOfType<Movement>();
        playerData = FindObjectOfType<PlayerData>();
        dash = FindObjectOfType<Dash>();
    }

    private void Update()
    {
        if (hit)
        {
            float step = bounceForce * Time.deltaTime;
            movement.gameObject.transform.position = Vector3.MoveTowards(movement.gameObject.transform.position, targetPos, step);

            if (Vector3.Distance(movement.gameObject.transform.position, targetPos) < 0.1)
            {
                characterController.enabled = true;
                hit = false;
                movement.offMove = false;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (dash.playerDashing && playerData.currentHealth != playerData.maxHealth)
            {
                playerData.Heal(healValue);

                if (destroyAfterDash)
                {
                    Destroy(this.gameObject);
                }
            }
            else if (!dash.playerDashing && thisObjectCanHitPlayer)
            {
                movement.offMove = true;
                characterController.enabled = false;
                other.GetComponent<Animator>().SetFloat("MoveSpeed", 0); // animacja
                playerData.TakeDamage(damageValue);
                targetPos = other.transform.position - other.gameObject.transform.forward * bounceDistance;
                hit = true;
            }
        }
    }
}
