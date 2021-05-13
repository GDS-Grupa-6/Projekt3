using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[RequireComponent(typeof(Movement))]
public class Shooting : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float shootForce = 20f;
    [SerializeField] [Range(0, 100)] private int reloadTime = 5;
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
    private int actualTime = 0;

    void Awake()
    {
        movement = GetComponent<Movement>();
        inputManager = InputManager.Instance;
        SetGFX(false);
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

            if (inputManager.PlayerShoot() && actualTime == 0)
            {
                CreateBullet();
                StartCoroutine(ReloadCourutine());
            }
        }
        else if (!movement.playerIsInShootPose && rigBuilder.enabled == true)
        {
            SetGFX(false);
        }
    }

    private IEnumerator ReloadCourutine()
    {
        actualTime = reloadTime;
        Debug.Log("Reolading gun: " + actualTime + "s");
        for (int i = actualTime; i >= 1; i--)
        {
            if (i > 1)
            {
                yield return new WaitForSeconds(1f);
                actualTime--;
                Debug.Log("Reolading gun: " + actualTime + "s");
            }
            else
            {
                yield return new WaitForSeconds(1f);
                actualTime = 0;
                Debug.Log("Gun is redy!");
            }
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
