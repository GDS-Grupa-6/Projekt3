using System.Collections;
using System.Collections.Generic;
using Raven.Config;
using Raven.Enemy;
using Raven.Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Raven.Manager
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private Transform _enemyGfxTransform;
        [SerializeField] private Material _gfxMaterial;
        [SerializeField] private Material _redMaterial;

        private Transform _player;
        private CoroutinesManager _coroutinesManager;
        private PlayerDataManager _playerDataManager;

        private float _currentHealth;

        [Inject]
        public void Construct(PlayerMovementManager p_playerMovementManager,CoroutinesManager p_coroutinesManager, PlayerDataManager p_playerDataManager)
        {
            _coroutinesManager = p_coroutinesManager;
            _player = p_playerMovementManager.PlayerTransform;
            _playerDataManager = p_playerDataManager;

            _currentHealth = _enemyConfig.MaxHealth;
        }

        private void Awake()
        {
            NavMeshAgent navMesh = GetComponent<NavMeshAgent>();

            switch (_enemyConfig.EnemyType)
            {
                case EnemyType.Kamikaze:
                    gameObject.AddComponent<FollowEnemy>().Initialization(_enemyConfig, _player, navMesh,
                        _enemyGfxTransform, _coroutinesManager, _playerDataManager);
                    break;

                case EnemyType.Shooter:
                    //TODO behaviour
                    break;
            }
        }

        public void TakeDamage(float p_value)
        {
            if (_currentHealth <= 0)
            {
                return;
            }

            _currentHealth -= p_value;
            _coroutinesManager.StartCoroutine(HitCoroutine(), this.gameObject);
        }

        private void Dead()
        {
            Destroy(this.gameObject);
        }

        private IEnumerator HitCoroutine()
        {
            _enemyGfxTransform.GetComponent<MeshRenderer>().materials[0] = _redMaterial;
            yield return new WaitForSeconds(1.5f);
            _enemyGfxTransform.GetComponent<MeshRenderer>().materials[0] = _gfxMaterial;

            if (_currentHealth <= 0)
            {
                if (_enemyConfig.EnemyType == EnemyType.Kamikaze)
                {
                    GetComponentInChildren<EnemyExplode>().Explode();
                }
                Dead();
            }
        }

        public class EnemyFactory : PlaceholderFactory<EnemyManager> { }
    }
}

