using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(CharacterController))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed = 40f;
    [SerializeField] private float dashTime = 0.25f;
    [SerializeField] private float cooldownTime = 1f;

    [HideInInspector] public bool playerDashing;
    private Movement movement;
    private CharacterController characterController;
    private InputManager inputManager;
    private bool canDash;

    void Start()
    {
        movement = GetComponent<Movement>();
        characterController = GetComponent<CharacterController>();
        inputManager = FindObjectOfType<InputManager>();
        canDash = true;
    }

    void Update()
    {
        if (inputManager.PlayerDash() && canDash)
        {
            canDash = false;
            StartCoroutine(DashCourutine());
        }
    }

    IEnumerator DashCourutine()
    {
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            characterController.Move(movement.moveDirection * dashSpeed * Time.deltaTime);
            playerDashing = true;
            yield return null;
        }

        playerDashing = false;
        StartCoroutine(CooldownCorutine());
    }

    IEnumerator CooldownCorutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        canDash = true;
    }
}
