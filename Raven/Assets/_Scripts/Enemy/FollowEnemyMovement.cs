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
        private float _delta = 0;
        private float _distance = 100;
        private float _speed;

        public void Construct(EnemyConfig p_enemyConfig, GameObject p_player)
        {
            _enemyConfig = p_enemyConfig;
            _playerTransform = p_player.GetComponent<Transform>();
            _speed = _enemyConfig.MoveSpeed;
        }

        private void Awake()
        {
            StartCoroutine(MoveDeltaCoroutine());
        }

        private void Update()
        {
            _distance = Vector3.Distance(_playerTransform.position, transform.position);
            Debug.Log(_distance);
            CalculateMovePoint();
            transform.LookAt(_playerTransform);
            transform.position += CalculateMovePoint() * _speed * Time.deltaTime;
        }

        private Vector3 CalculateMovePoint()
        {
            return transform.forward + transform.right * _delta;
        }

        private IEnumerator MoveDeltaCoroutine()
        {
            while (_distance > 5)
            {
                yield return new WaitForSeconds(1);
                switch (_delta)
                {
                    case 0:
                        _delta = 1;
                        break;
                    case 1:
                        _delta = -1;
                        break;
                    case -1:
                        _delta = 0;
                        break;
                    default:
                        break;
                }
            }

            _delta = 0;
            _speed = _enemyConfig.MoveSpeed * 5f;
        }
    }
}

