using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaves : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private Wave _wave360;
    [SerializeField] private Wave _wave45;
    [Header("Wave360 options")]
    [SerializeField] private Vector3 _maxScaleWave360;
    [SerializeField] private float _growSpeedWave360;
    [SerializeField] private float _powerWave360;
    [Header("Wave45 options")]
    [SerializeField] private Vector3 _maxScaleWave45;
    [SerializeField] private float _growSpeedWave45;
    [SerializeField] private float _powerWave45;

    private Transform _wave360Transform;
    private Transform _wave45Transform;

    private void Awake()
    {
        _wave360.power = _powerWave360;
        _wave45.power = _powerWave45;

        _wave360Transform = _wave360.GetComponent<Transform>();
        _wave45Transform = _wave45.GetComponent<Transform>();
    }

    public void ScaleWave(bool wave360)
    {
        if (wave360)
        {
            _wave360Transform.localScale = Vector3.Lerp(_wave360Transform.transform.localScale, _maxScaleWave360, Time.deltaTime * _growSpeedWave360);
        }
        else
        {
            _wave45Transform.localScale = Vector3.Lerp(_wave45Transform.transform.localScale, _maxScaleWave45, Time.deltaTime * _growSpeedWave45);
        }
    }
}
