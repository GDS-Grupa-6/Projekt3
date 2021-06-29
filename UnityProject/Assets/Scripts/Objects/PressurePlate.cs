using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float _speed = 5;
    [Header("Plate settings")]
    [SerializeField] private Transform _plateMovingPartTransform;
    [SerializeField] private float _platePressedY;
    [Header("Door settings")]
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private float _doorOpenedY;

    private Vector3 _doorOpenedPosition;
    private Vector3 _platePressedPosition;
    private float _doorStartY;
    private float _plateStartY;
    private bool _doorOpening;
    private bool _doorClosing;
    private bool _doorAreOpen;

    private void Awake()
    {
        _doorAreOpen = false;
        _doorClosing = false;
        _doorOpening = false;

        _doorStartY = _doorTransform.position.y;
        _plateStartY = _plateMovingPartTransform.localPosition.y;

        _doorOpenedPosition = new Vector3(_doorTransform.position.x, _doorOpenedY, _doorTransform.position.z);
        _platePressedPosition = new Vector3(_plateMovingPartTransform.localPosition.x, _platePressedY, _plateMovingPartTransform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_doorAreOpen)
            {
                _doorOpening = true;
                StartCoroutine(Opening());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            if (_doorOpening)
            {
                _doorOpening = false;
                StopAllCoroutines();
            }
        }
    }

    private IEnumerator Opening()
    {
        float startTime = Time.time;
        float endTime = startTime + _speed;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / _speed;
            _doorTransform.position = Vector3.Lerp(_doorTransform.position, _doorOpenedPosition, timeProgressed);
            _plateMovingPartTransform.position = Vector3.Lerp(_plateMovingPartTransform.position, _platePressedPosition, timeProgressed);
            yield return new WaitForFixedUpdate();
        }

        Vector3 centerPos = _doorTransform.position;
        centerPos.y = _doorOpenedY;
        _doorTransform.position = centerPos;

        _doorAreOpen = true;
        _doorOpening = false;
    }

    private void CloseDoor()
    {

    }
}
