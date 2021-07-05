using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossMovement : MonoBehaviour
{
    [Header("Move points")]
    [SerializeField] private Transform _centerOfArena;
    [Header("Jump options")]
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private float _jumpSpeed = 5f;

    [HideInInspector] public bool _bossJump;
    [HideInInspector] public Vector3 _bossMoveTarget;

    private Vector3 _bossStartJumpPos;
    private float _timeParabolaJump;
    private Animator _animator;

    void Awake()
    {
        _animator = GetComponent<Animator>();
        _bossMoveTarget = _centerOfArena.localPosition;
    }

    void Update()
    {
        _animator.SetBool("BossJump", _bossJump);

        if (_bossJump)
        {
            ParabolaJump(_bossMoveTarget, _bossStartJumpPos);
        }
        else
        {
            _bossStartJumpPos = transform.position;
        }
    }

    public void ParabolaJump(Vector3 target, Vector3 startPos)
    {
        _timeParabolaJump += Time.deltaTime;
        _timeParabolaJump = _timeParabolaJump % 5f;

        if (Vector3.Distance(transform.position, target) > 1f)
        {
            transform.position = Parabola(startPos, target, _jumpHeight, (_timeParabolaJump / 5) * _jumpSpeed);
        }
        else
        {
            _bossJump = false;
        }
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
