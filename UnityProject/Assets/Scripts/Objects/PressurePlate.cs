using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] private float _openTime = 20;
    [Header("Plate settings")]
    [SerializeField] private Transform _plateMovingPartTransform;
    [SerializeField] private float _plateMovingPartPressedY;
    [Header("Door settings")]
    [SerializeField] private Transform _doorTransform;
    [SerializeField] private float _doorOpenedY;

    private Vector3 _plateStartPos;
    private Vector3 _doorStartPos;
    private Vector3 _doorOpenedPosition;
    private Vector3 _platePressedPosition;
    private float _doorStartY;
    private float _plateStartY;
    private bool _doorOpening;
    private bool _doorAreOpen;

    private void Awake()
    {
        _doorAreOpen = false;
        _doorOpening = false;

        _doorStartY = _doorTransform.position.y;
        _plateStartY = _plateMovingPartTransform.localPosition.y;

        _doorStartPos = _doorTransform.position;
        _doorOpenedPosition = new Vector3(_doorTransform.position.x, _doorOpenedY, _doorTransform.position.z);
        _plateStartPos = _plateMovingPartTransform.localPosition;
        _platePressedPosition = new Vector3(_plateMovingPartTransform.localPosition.x, _plateMovingPartPressedY, _plateMovingPartTransform.localPosition.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (!_doorAreOpen)
            {
                _doorOpening = true;
                StopAllCoroutines();
                StartCoroutine(OpeningCorutine());
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
                _doorAreOpen = false;
                StopAllCoroutines();
                StartCoroutine(ClosingCorutine());
            }
        }
    }

    private IEnumerator OpeningCorutine()
    {
        float startTime = Time.time;
        float endTime = startTime + _openTime;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / _openTime;
            _doorTransform.position = Vector3.Lerp(_doorTransform.position, _doorOpenedPosition, timeProgressed);
            _plateMovingPartTransform.localPosition = Vector3.Lerp(_plateMovingPartTransform.localPosition, _platePressedPosition, timeProgressed);
            yield return new WaitForFixedUpdate();
        }

        Vector3 centerPos = _doorTransform.position;
        centerPos.y = _doorOpenedY;
        _doorTransform.position = centerPos;

        centerPos = _plateMovingPartTransform.localPosition;
        centerPos.y = _plateMovingPartPressedY;
        _plateMovingPartTransform.localPosition = centerPos;

        _doorAreOpen = true;
        _doorOpening = false;
    }

    private IEnumerator ClosingCorutine()
    {
        float startTime = Time.time;
        float endTime = startTime + _openTime;

        while (Time.time < endTime)
        {
            float timeProgressed = (Time.time - startTime) / _openTime;
            _doorTransform.position = Vector3.Lerp(_doorTransform.position, _doorStartPos, timeProgressed);
            _plateMovingPartTransform.localPosition = Vector3.Lerp(_plateMovingPartTransform.localPosition, _plateStartPos, timeProgressed);
            yield return new WaitForFixedUpdate();
        }

        Vector3 centerPos = _doorTransform.position;
        centerPos.y = _doorStartY;
        _doorTransform.position = centerPos;

        centerPos = _plateMovingPartTransform.localPosition;
        centerPos.y = _plateStartY;
        _plateMovingPartTransform.localPosition = centerPos;

        _doorAreOpen = false;
        _doorOpening = false;
    }
}
