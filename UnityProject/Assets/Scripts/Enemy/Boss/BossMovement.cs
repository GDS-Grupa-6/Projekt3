using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float _parabolaJumpHeight = 10f;
    [SerializeField] private float _jumpSpeed = 5f;
    [SerializeField] private float _speed = 3f;

    [HideInInspector] public Transform player;
    [HideInInspector] public float timeParabolaJump;
    [HideInInspector] public bool parabolaJumpEnded;

    private void Awake()
    {
        player = FindObjectOfType<CharacterControllerLogic>().transform;
    }

    public void Walk()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * _speed);
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    public void Teleport(Transform target)
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }

    public void ParabolaJump(Vector3 target, Vector3 startPos)
    {
        timeParabolaJump += Time.deltaTime;
        timeParabolaJump = timeParabolaJump % 5f;

        if (Vector3.Distance(transform.position, target) > 1f)
        {
            parabolaJumpEnded = false;
            transform.position = Parabola(startPos, target, _parabolaJumpHeight, (timeParabolaJump / 5) * _jumpSpeed);
        }
        else
        {
            parabolaJumpEnded = true;
            return;
        }
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
