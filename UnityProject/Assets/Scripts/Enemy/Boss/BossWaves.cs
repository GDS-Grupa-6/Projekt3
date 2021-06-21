using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWaves : MonoBehaviour
{
    [Header("Waves")]
    [SerializeField] private Wave _wave360;
    [SerializeField] private Wave _wave45;
    public Vector3 maxScaleWave360;
    [SerializeField] private float _growSpeedWave360;
    [SerializeField] private float _powerWave360;
    [Header("Wave45 options")]
    public Vector3 maxScaleWave45;
    [SerializeField] private float _growSpeedWave45;
    [SerializeField] private float _powerWave45;

    [HideInInspector] public Transform wave360Transform;
    [HideInInspector] public Transform wave45Transform;

    private void Awake()
    {
        _wave360.power = _powerWave360;
        _wave45.power = _powerWave45;

        wave360Transform = _wave360.GetComponent<Transform>();
        wave45Transform = _wave45.GetComponent<Transform>();
    }

    public void ScaleWave(bool wave360)
    {
        if (wave360)
        {
            wave360Transform.localScale = Vector3.Lerp(wave360Transform.transform.localScale, maxScaleWave360, Time.deltaTime * _growSpeedWave360);
        }
        else
        {
            wave45Transform.localScale = Vector3.Lerp(wave45Transform.transform.localScale, maxScaleWave45, Time.deltaTime * _growSpeedWave45);
        }
    }
}
