using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float _parabolaJumpHeight = 10f;
    [SerializeField] private float _jumpSpeed = 5f;

    [HideInInspector] public Vector3 startBossPosition;
    [HideInInspector] public Transform player;
    [HideInInspector] public float timeParabolaJump;

    private void Awake()
    {
        player = FindObjectOfType<CharacterControllerLogic>().transform;
    }

    public void Teleport(Transform targetTransform)
    {
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }

    public void ParabolaJump(Transform targetTransform)
    {
        timeParabolaJump += Time.deltaTime;
        timeParabolaJump = timeParabolaJump % 5f;

        transform.position = Parabola(startBossPosition, targetTransform.position, _parabolaJumpHeight, (timeParabolaJump / 5) * _jumpSpeed);
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
