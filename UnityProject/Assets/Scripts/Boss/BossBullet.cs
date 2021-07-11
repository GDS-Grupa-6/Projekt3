using System;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float _power = 10f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _parabolaHeight = 10f;

    private float _time;

    [HideInInspector] public Vector3 target;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public bool fly;
    [HideInInspector] public BossCombatLogic combatLogic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerData>().TakeDamage(_power);
        }

        if (other.tag != "BossBullet" && other.tag != "PukeSphere" && other.tag != "Boss")
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (fly)
        {
            _time += Time.deltaTime;
            _time = _time % 5f;

            if (Vector3.Distance(transform.position, target) > 0.1f)
            {
                transform.position = Parabola(startPos, target, _parabolaHeight, (_time / 5) * _speed);
            }
            else
            {       
                Destroy(this.gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        combatLogic.destroyedBullets++;
    }

    private Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;

        var mid = Vector3.Lerp(start, end, t);

        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }
}
