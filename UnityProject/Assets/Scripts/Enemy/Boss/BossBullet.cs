using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [HideInInspector] public int power;
    [HideInInspector] public PlayerData playerData;
    [HideInInspector] public Vector3 endPosition;
    [HideInInspector] public Vector3 startPosition;
    [HideInInspector] public float speed;
    [HideInInspector] public float flyHeight;
    [HideInInspector] public bool fly;
    [HideInInspector] public BossMovement bossMovement;

    private float _time;

    private void Update()
    {
        if (fly)
        {
            Fly();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerData.TakeDamage(power);
        }

        if (other.tag != this.gameObject.tag && other.tag != "Boss")
        {
            Destroy(this.gameObject);
        }
    }

    private void Fly()
    {
        _time += Time.deltaTime;
        _time = _time % 5f;

        transform.position = bossMovement.Parabola(startPosition, endPosition, flyHeight, (_time / 5) * speed);
    }
}
