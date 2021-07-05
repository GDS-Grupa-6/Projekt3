using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BossMovement : MonoBehaviour
{
    [Header("Move points")]
    [SerializeField] private float _speed;
    [Header("Jump options")]
    [SerializeField] private Transform _centerOfArena;
    [SerializeField] private float _jumpHeight = 10f;
    [SerializeField] private float _jumpSpeed = 5f;

    [HideInInspector] public bool bossJump;
    [HideInInspector] public bool moveBoss;
    [HideInInspector] public Vector3 bossMoveTarget;
    [HideInInspector] public GameObject player;

    private Vector3 _bossStartJumpPos;
    private float _timeParabolaJump;
    private Animator _animator;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        player = FindObjectOfType<CharacterController>().gameObject;
        _animator = GetComponent<Animator>();
        bossMoveTarget = _centerOfArena.localPosition;
    }

    private void Update()
    {
        _animator.SetBool("BossJump", bossJump);

        if (bossJump)
        {
            ParabolaJump(bossMoveTarget, _bossStartJumpPos);
        }
        else
        {
            _bossStartJumpPos = transform.position;
        }

        if (moveBoss)
        {
            Move();
        }
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }

    private void Move()
    {
        bossMoveTarget = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.position = Vector3.MoveTowards(transform.position, bossMoveTarget, Time.deltaTime * _speed);
        transform.LookAt(bossMoveTarget);
    }

    private void ParabolaJump(Vector3 target, Vector3 startPos)
    {
        _timeParabolaJump += Time.deltaTime;
        _timeParabolaJump = _timeParabolaJump % 5f;

        if (Vector3.Distance(transform.position, target) > 1f)
        {
            transform.position = Parabola(startPos, target, _jumpHeight, (_timeParabolaJump / 5) * _jumpSpeed);
        }
        else
        {
            bossJump = false;
            _timeParabolaJump = 0;
            return;
        }
    }

    public float DistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }
}
