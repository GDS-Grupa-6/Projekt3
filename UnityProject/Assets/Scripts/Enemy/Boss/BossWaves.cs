using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BossCombatLogic))]
[RequireComponent(typeof(Animator))]
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

    private Vector3 _startScale;
    private Animator _animator;
    private BossCombatLogic _bossCombatLogic;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _bossCombatLogic = GetComponent<BossCombatLogic>();

        _wave360.power = _powerWave360;
        _wave45.power = _powerWave45;

        wave360Transform = _wave360.GetComponent<Transform>();
        wave45Transform = _wave45.GetComponent<Transform>();
    }

    private void DesactiveWave(bool wave360)
    {
        if (wave360)
        {
            _wave360.gameObject.SetActive(false);
            wave360Transform.localScale = _startScale;
        }
        else
        {
            _wave45.gameObject.SetActive(false);
            wave45Transform.localScale = _startScale;
        }
    }

    public void ActiveWave(bool wave360)
    {
        if (wave360)
        {
            _startScale = wave360Transform.localScale;
            _wave360.gameObject.SetActive(true);
        }
        else
        {
            _startScale = wave45Transform.localScale;
            _wave45.gameObject.SetActive(true);
        }
    }

    public void ScaleWave(bool wave360)
    {
        if (wave360)
        {
            wave360Transform.localScale = Vector3.Lerp(wave360Transform.transform.localScale, maxScaleWave360, Time.deltaTime * _growSpeedWave360);

            if (wave360Transform.localScale.x >= maxScaleWave360.x - 10f)
            {
                DesactiveWave(true);
                _bossCombatLogic.CheckDistance();
                _animator.SetBool("Wave360End", true);
                return;
            }
        }
        else
        {
            wave45Transform.localScale = Vector3.Lerp(wave45Transform.transform.localScale, maxScaleWave45, Time.deltaTime * _growSpeedWave45);

            if (wave45Transform.localScale.x >= maxScaleWave45.x - 10f)
            {
                DesactiveWave(false);
                _animator.SetBool("Wave45End", true);
                return;
            }
        }
    }
}
