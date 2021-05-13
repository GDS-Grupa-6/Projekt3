using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Movement))]
public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bulletPrifab;
    [SerializeField] private GameObject viewfinder;
    [SerializeField] private Camera mainCam;
    [SerializeField] private float shootForce = 20f;
    [SerializeField] private Rig gunRig;
    [SerializeField] private RigBuilder rigBuilder;
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunGFX;

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
        if (movement.playerIsInShootPose)
        {
            gunGFX.SetActive(true);
            // GunLookAt();

            if (rigBuilder.enabled == false)
            {
                rigBuilder.enabled = true;
            }

            viewfinder.SetActive(true);
            gunRig.weight = 1;

            if (inputManager.PlayerShoot())
            {
                CreateBullet();
            }
        }
        else
        {
            gunGFX.SetActive(false);
            viewfinder.SetActive(false);
            gunRig.weight = 0;

            if (rigBuilder.enabled == true)
            {
                rigBuilder.enabled = false;
            }
        }
    }

    private void CreateBullet()
    {
        var obj = Instantiate(bulletPrifab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.velocity = mainCam.transform.forward * shootForce;
    }

    private void GunLookAt()
    {
        gun.transform.LookAt(new Vector3(Camera.main.ScreenToWorldPoint(viewfinder.transform.position).x,
            Camera.main.ScreenToWorldPoint(viewfinder.transform.position).y,
            -Camera.main.ScreenToWorldPoint(viewfinder.transform.position).z));
    }
}
