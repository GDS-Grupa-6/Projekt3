using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Movement))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private float shootForce = 20f;
    [Header("Gun options")]
    [SerializeField] private Rig gunRig;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunGFX;
    [SerializeField] private GameObject bulletPrifab;
    [Header("Camera options")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject viewfinder;
    [Header("Player rig")]
    [SerializeField] private RigBuilder rigBuilder;


    private InputManager inputManager;
    private Movement movement;

    void Awake()
    {
        movement = GetComponent<Movement>();
        inputManager = InputManager.Instance;
        gunGFX.SetActive(false);
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (movement.playerIsInShootPose)
        {
            if (rigBuilder.enabled == false)
            {
                SetGFX(true);
            }

            if (inputManager.PlayerShoot())
            {
                CreateBullet();
            }
        }
        else if (!movement.playerIsInShootPose && rigBuilder.enabled == true)
        {
            SetGFX(false);
        }
    }

    private void CreateBullet()
    {
        var obj = Instantiate(bulletPrifab, gun.transform.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = -gun.GetComponent<Gun>().targetTransform.up * shootForce;
    }

    private void SetGFX(bool setActive)
    {
        gunGFX.SetActive(setActive);
        viewfinder.SetActive(setActive);
        rigBuilder.enabled = setActive;
    }
}
