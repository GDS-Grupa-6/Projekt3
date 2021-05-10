using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrifab;

    private InputManager inputManager;
    private Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
        inputManager = InputManager.Instance;

    }

    void Update()
    {
        if (movement.playerIsInShootPose && inputManager.PlayerShoot())
        {
            var obj = Instantiate(bulletPrifab);
            obj.transform.position = shootPoint.position;
            obj.transform.rotation = shootPoint.rotation;
        }
    }
}
