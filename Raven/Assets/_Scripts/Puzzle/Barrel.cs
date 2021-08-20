using System.Collections;
using System.Collections.Generic;
using Raven.Enemy;
using UnityEngine;

namespace Raven.Puzzle
{
    [RequireComponent(typeof(CapsuleCollider))]
    [RequireComponent(typeof(Explode))]
    public class Barrel : MonoBehaviour
    {
        [SerializeField] private GameObject[] _destroyable;

        private Collider _collider;
        private Explode _explode;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.isTrigger = true;
            _explode = GetComponent<Explode>();
        }

        private void OnTriggerEnter(Collider p_other)
        {
            if (p_other.tag == "Bullet" || p_other.tag == "FireBullet")
            {
                _explode.ExplodeBehaviour();

                for (int i = 0; i < _destroyable.Length; i++)
                {
                    Destroy(_destroyable[i]);
                }

                Destroy(gameObject);
            }
        }
    }
}