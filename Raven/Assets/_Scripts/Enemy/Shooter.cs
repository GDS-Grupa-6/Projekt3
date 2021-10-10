using Raven.Config;
using Raven.Core;
using Raven.Core.Interfaces;
using Raven.Player;
using UnityEngine;
using UnityEngine.AI;

namespace Raven.Enemy
{
    public class Shooter : IEnemy
    {
        private GameObject _enemy;
        private Transform _playerTransform;
        private Transform _gfxTransform;
        private EnemyConfig _enemyConfig;
        private NavMeshAgent _navMesh;
        private Transform _shootPoint;
        private PlayerDataManager _playerDataManager;
        private AudioManager _audioManager;
        private AudioClipConditions[] _audioClipConditions;
        private AudioSource[] _audioSource;

        private float _timer;

        public Shooter(EnemyConfig p_enemyConfig, Transform p_player, NavMeshAgent p_navMesh, Transform p_gfxTransform, PlayerDataManager p_playerDataManager, GameObject p_enemy, Transform p_shootPoint,
             AudioManager p_audioManager, AudioClipConditions[] p_audioClipConditions, AudioSource[] p_audioSource)
        {
            _audioSource = p_audioSource;
            _audioManager = p_audioManager;
            _audioClipConditions = p_audioClipConditions;
            _playerDataManager = p_playerDataManager;
            _shootPoint = p_shootPoint;
            _enemy = p_enemy;
            _gfxTransform = p_gfxTransform;
            _navMesh = p_navMesh;
            _enemyConfig = p_enemyConfig;
            _playerTransform = p_player;
        }

        public void Behaviour()
        {
            float distance = Vector3.Distance(_enemy.transform.position, _playerTransform.position);
            Vector3 lookAtVector3 = _playerTransform.position;
            lookAtVector3.y += 1.62f;

            _gfxTransform.LookAt(lookAtVector3);

            if (distance > _enemyConfig.ShootDistance)
            {
                _timer = float.MaxValue;

                if (!_enemyConfig.IsStatic)
                {
                    _navMesh.speed = _enemyConfig.MoveSpeed;
                    _navMesh.SetDestination(_playerTransform.position);
                }
            }
            else
            {
                _navMesh.speed = 0;
                Shoot();
            }
        }

        private void Shoot()
        {
            if (_timer < _enemyConfig.ShootDelayTime)
            {
                _timer += Time.deltaTime;
            }
            else
            {
                _audioManager.PlaySound(_audioManager.GetCurrenAudioClipConditions(_audioClipConditions, AudioNames.Shoot), _audioSource[1]);
                var obj = Object.Instantiate(_enemyConfig.Bullet, _shootPoint.position, _shootPoint.rotation);
                obj.GetComponent<Bullet>().Initialization(_enemyConfig.BulletSpeed, _playerDataManager, _enemyConfig, _gfxTransform.gameObject);
                _timer = 0;
            }
        }
    }
}

