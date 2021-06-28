using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Plate settings")]
    [SerializeField] private Transform _plateMovingPartTransform;
    [SerializeField] private float _plateSpeed;
    [SerializeField] private float _plateDeltaY;
    [Header("Door settings")]
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private float _doorSpeed;
    [SerializeField] private float _doorDeltaY;

    private float _doorStartY;
    private float _plateStartY;

    private void DesactivePlate()
    {

    }

    private void ActivePlate()
    {

    }

    private void OpenDoor()
    {

    }

    private void CloseDoor()
    {

    }
}
