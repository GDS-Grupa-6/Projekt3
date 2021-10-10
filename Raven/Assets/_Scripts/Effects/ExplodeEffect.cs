using Raven.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeEffect : MonoBehaviour
{
    [SerializeField, Range(1, 10)] private float _effectTime = 2f;
    [SerializeField] private AudioClipConditions _explodeClip;

    private float _timer;

    public void Init(AudioManager p_audioManager)
    {
        p_audioManager.PlaySound(_explodeClip, GetComponent<AudioSource>());
    }

    private void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _effectTime)
        {
            Destroy(gameObject);
        }
    }
}
