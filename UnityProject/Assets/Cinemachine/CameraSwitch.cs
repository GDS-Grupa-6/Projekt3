﻿using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class CameraSwitch : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private Movement player;
    [Header("Switch camera input")]
    [SerializeField] private InputAction action;
    [Header("Cameras")]
    [SerializeField] private Transform shootCamera;
    [SerializeField] private CinemachineFreeLook tppCamera;
    [SerializeField] private Vector2 camerasDelta;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        action.performed += _ => SwitchState();

        player.playerIsInShootPose = false;
    }

    private void OnEnable()
    {
        action.Enable();
    }
    private void OnDisable()
    {
        action.Disable();
    }

    private void SwitchState()
    {
        if (!player.playerIsInShootPose)
        {
            player.transform.rotation = Quaternion.Euler(0, tppCamera.m_XAxis.Value, 0);
            animator.Play("ShootCamera");
            player.playerIsInShootPose = true;
        }
        else
        {
            player.playerIsInShootPose = false;
            Vector3 pos = player.transform.rotation.eulerAngles;
            tppCamera.m_XAxis.Value = pos.y;
            animator.Play("TppCamera");
        }
    }
}
