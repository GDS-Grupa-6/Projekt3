using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NaughtyAttributes;
using Raven.Config;
using Raven.Core.Interfaces;
using Raven.Enemy;
using Raven.Player;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Raven.Manager
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyController : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private Transform _enemyGfxTransform;
        [SerializeField] private Material _gfxMaterial;
        [SerializeField] private Material _redMaterial;
        [SerializeField] private bool _staticActivator;

        [ShowIf("_staticActivator"), SerializeField] private float _activateRadius;

        private Transform _player;
        private CoroutinesManager _coroutinesManager;
        private PlayerDataManager _playerDataManager;
        private IEnemy _enemyBehaviour;
        private bool _active;
        private float _currentHealth;



        [Inject]
        public void Construct(PlayerMovementManager p_playerMovementManager, CoroutinesManager p_coroutinesManager, PlayerDataManager p_playerDataManager)
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
                    _enemyBehaviour = new Kamikaze(_enemyConfig, _player, navMesh,
                           _enemyGfxTransform, _coroutinesManager, _playerDataManager, gameObject);
                    break;

                case EnemyType.Shooter:
                    //TODO behaviour
                    break;
            }
        }

        public void ActivateEnemy()
        {
            _active = true;
            _enemyBehaviour.Start();
        }

        private void Update()
        {
            if (_active)
            {
                _enemyBehaviour.Update();
            }
            else if (_staticActivator)
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, _activateRadius, transform.forward,_activateRadius);

                if (hits.Any(x => x.collider.gameObject.tag == "Player"))
                {
                    _active = true;
                }
            }
        }

        public void TakeDamage(float p_value)
        {
            if (_currentHealth <= 0)
            {
                return;
            }

            _currentHealth -= p_value;

            if (_currentHealth <= 0)
            {
                if (_enemyConfig.EnemyType == EnemyType.Kamikaze)
                {
                    GetComponentInChildren<EnemyExplode>().Explode();
                }

                Dead();
            }
        }

        private void Dead()
        {
            Destroy(this.gameObject);
        }

        /*private IEnumerator HitCoroutine()
        {
            _enemyGfxTransform.GetComponent<MeshRenderer>().materials[0] = _redMaterial;
            yield return new WaitForSeconds(1.5f);
            _enemyGfxTransform.GetComponent<MeshRenderer>().materials[0] = _gfxMaterial;
        }*/

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_staticActivator)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(transform.position, _activateRadius);
            }
        }
#endif

        public class EnemyFactory : PlaceholderFactory<EnemyController> { }
    }
}

