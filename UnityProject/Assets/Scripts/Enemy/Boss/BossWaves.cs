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
    [SerializeField] private float _growTimeWave360;
    [SerializeField] private float _powerWave360;
    [Header("Wave45 options")]
    public Vector3 maxScaleWave45;
    [SerializeField] private float _growTimeWave45;
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

    public void StartWave(bool wave360)
    {
        if (wave360)
        {
            StartCoroutine(Wave360Courutine());
        }
        else
        {
            StartCoroutine(Wave45Courutine());
        }
    }

    private IEnumerator Wave360Courutine()
    {
        float startTime = Time.time;
        float endTime = startTime + _growTimeWave360;

        while (Time.time < endTime && wave360Transform.localScale != maxScaleWave360)
        {
            float timeProgressed = (Time.time - startTime) / _growTimeWave360;
            wave360Transform.localScale = Vector3.Slerp(wave360Transform.localScale, maxScaleWave360, timeProgressed);
            yield return new WaitForFixedUpdate();
        }

        DesactiveWave(true);
        _bossCombatLogic.CheckDistance();
        _animator.SetBool("Wave360End", true);
    }

    private IEnumerator Wave45Courutine()
    {
        float startTime = Time.time;
        float endTime = startTime + _growTimeWave45;

        while (Time.time < endTime && wave45Transform.localScale != maxScaleWave45)
        {
            float timeProgressed = (Time.time - startTime) / _growTimeWave45;
            wave45Transform.localScale = Vector3.Slerp(wave45Transform.localScale, maxScaleWave45, timeProgressed);
            yield return new WaitForFixedUpdate();
        }

        DesactiveWave(false);
        _animator.SetBool("Wave45End", true);
    }
}
