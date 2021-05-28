using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using System;

public class Shooting : MonoBehaviour
{
    [SerializeField] [Range(0, 100)] private float shootForce = 20f;
    [SerializeField] [Range(0, 100)] private float reloadTime = 5;
    [Header("Gun options")]
    [SerializeField] private GameObject gun;
    [SerializeField] private GameObject gunGFX;
    [SerializeField] private GameObject bulletPrifab;
    [Header("Camera options")]
    [SerializeField] private Camera mainCam;
    [SerializeField] private GameObject viewfinder;
    [Header("Player rig")]
    [SerializeField] private RigBuilder rigBuilder;

    private InputManager inputManager;
    private CameraSwitch cameraSwitch;
    private Gun gunScript;
    private float actualTime = 0;

    void Awake()
    {
        gunScript = gun.GetComponent<Gun>();
        cameraSwitch = FindObjectOfType<CameraSwitch>();
        inputManager = FindObjectOfType<InputManager>();
        SetGFX(false);
    }

    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (cameraSwitch.playerIsInShootPose)
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
        else if (!cameraSwitch.playerIsInShootPose && rigBuilder.enabled == true)
        {
            SetGFX(false);
        }
    }

    private IEnumerator ReloadCourutine()
    {
        actualTime = reloadTime;
        Debug.Log("Reolading gun: " + actualTime + "s");
        for (float i = actualTime; i >= 0; i -= 0.1f)
        {
            if (i > 0)
            {
                yield return new WaitForSeconds(0.1f);
                i = (float)Math.Round(i, 1);
                actualTime = i;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
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
        gunScript.enabled = setActive;
        gunGFX.SetActive(setActive);
        viewfinder.SetActive(setActive);
        rigBuilder.enabled = setActive;
    }
}
