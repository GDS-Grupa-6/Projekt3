using System;
using System.Collections;
using System.Collections.Generic;
using Raven.Config;
using UnityEngine;

namespace Raven.Enemy
{
    public class FollowEnemyMovement : MonoBehaviour
    {
        private EnemyConfig _enemyConfig;
        private Transform _playerTransform;

        private List<Vector3> _pathPoints = new List<Vector3>();

        private float _timer;
        private float _delta = 1;

        public void Construct(EnemyConfig p_enemyConfig, GameObject p_player)
        {
            _enemyConfig = p_enemyConfig;
            _playerTransform = p_player.GetComponent<Transform>();
        }

        private void Awake()
        {
            StartCoroutine(MoveDeltaCoroutine());
        }

        private void Update()
        {
            CalculateMovePoint();
            transform.LookAt(_playerTransform);
            transform.position += CalculateMovePoint() * _enemyConfig.MoveSpeed * Time.deltaTime;
        }

        private Vector3 CalculateMovePoint()
        {
            return transform.forward + transform.right * _delta;
        }

        private IEnumerator MoveDeltaCoroutine()
        {
            while (true)
            {
                yield return new WaitForSeconds(1);
                if (_delta - 1 > -2)
                {
                    _delta--;
                }
                else
                {
                    _delta = 1;
                }
            }
        }
    }
}

