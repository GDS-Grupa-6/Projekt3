using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [HideInInspector] public Transform bossTargetPosition;
    [HideInInspector] public Transform player;

    private void Awake()
    {
        player = FindObjectOfType<CharacterControllerLogic>().transform;
    }

    public void Teleport()
    {
        transform.position = bossTargetPosition.position;
        transform.rotation = bossTargetPosition.rotation;
    }
}
