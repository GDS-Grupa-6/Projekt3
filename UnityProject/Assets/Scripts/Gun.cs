using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform targetTransform;
    [SerializeField] private Transform aimTransform;
    [SerializeField] private Transform bone;

    private int iterations = 10;

    private void LateUpdate()
    {
        Vector3 targetPosition = targetTransform.position;

        for (int i = 0; i < iterations; i++)
        {
            AimAtTarget(bone, targetPosition);
        }
    }

    private void AimAtTarget(Transform bone, Vector3 targetPosition)
    {
        Vector3 aimDirection = aimTransform.forward;
        Vector3 targetDirection = targetPosition - aimTransform.position;
        Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
        bone.rotation = aimTowards * bone.rotation;
    }
}
