using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour
{
    [SerializeField] private float parabolaJumpHeight = 10f;
    [SerializeField] private float jumpSpeed = 5f;

    [HideInInspector] public Transform bossTargetTransform;
    [HideInInspector] public Transform player;
    [HideInInspector] public float timeParabolaJump;
    [HideInInspector] public Vector3 startBossPosition;

    private void Awake()
    {
        player = FindObjectOfType<CharacterControllerLogic>().transform;
    }

    public void Teleport()
    {
        transform.position = bossTargetTransform.position;
        transform.rotation = bossTargetTransform.rotation;
    }

    public void ParabolaJump()
    {
        timeParabolaJump += Time.deltaTime;
        timeParabolaJump = timeParabolaJump % 5f;

        transform.position = Parabola(startBossPosition, bossTargetTransform.position, parabolaJumpHeight, (timeParabolaJump / 5) * jumpSpeed);
    }

    public static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
