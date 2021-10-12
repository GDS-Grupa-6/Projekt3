using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checpoint : MonoBehaviour
{
    [SerializeField] private ResetPoint[] _resetPoints;
    [SerializeField] private Animator _resetPanel;

    private void Awake()
    {
        for (int i = 0; i < _resetPoints.Length; i++)
        {
            _resetPoints[i].ResetPanel = _resetPanel;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            for (int i = 0; i < _resetPoints.Length; i++)
            {
                _resetPoints[i].ResetPosition = other.transform.position;
                _resetPoints[i].ResetRotation = other.transform.rotation;
                _resetPoints[i].PlayerTransform = other.transform;
            }

          //  Destroy(this.gameObject);
        }
    }
}
