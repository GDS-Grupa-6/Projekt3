using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
[RequireComponent(typeof(CharacterController))]
public class Dash : MonoBehaviour
{
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;

    [HideInInspector] public bool playerDashing;
    private Movement movement;
    private CharacterController characterController;
    private InputManager inputManager;

    void Start()
    {
        movement = GetComponent<Movement>();
        characterController = GetComponent<CharacterController>();
        inputManager = FindObjectOfType<InputManager>();
    }

    void Update()
    {
        if (inputManager.PlayerDash())
        {
            if (dashSpeed <= movement.speed)
            {
                Debug.Log("Prędkość dasha nie może być mniejsza lub równa prędkości ruchu");
            }
            else
            {
                StartCoroutine(DashCourutine());
            }
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
    }
}
