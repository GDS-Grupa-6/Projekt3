using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _effectTime = 2f;

    private float _timer;

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _effectTime)
        {
            Destroy(gameObject);
        }
    }
}
