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
        [Space]
        [SerializeField, BoxGroup("-----GFX-----")] private Transform _enemyGfxTransform;
        [SerializeField, BoxGroup("-----GFX-----")] private Material _gfxMaterial;
        [SerializeField, BoxGroup("-----GFX-----")] private Material _redMaterial;
        [Space]
        [SerializeField, BoxGroup("-----POV-----")] private float _activateRadius;
        [SerializeField, BoxGroup("-----POV-----")] private float _activateAngle = 90;
        [SerializeField, BoxGroup("-----POV-----")] private LayerMask _whatCanSee;
        [Space]
        [SerializeField, BoxGroup("-----FOR SHOOTER-----"), ShowIf("_isShooter")] private Transform _shootPoint;

        private bool _isShooter => _enemyConfig.EnemyType == EnemyType.Shooter;

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
                    _enemyBehaviour = new Shooter(_enemyConfig, _player, navMesh,
                        _enemyGfxTransform, _playerDataManager, gameObject, _shootPoint);
                    break;
            }
        }

        private void Update()
        {
            if (_active)
            {
                _enemyBehaviour.Behaviour();
                return;
            }

            Collider[] hit = Physics.OverlapSphere(_enemyGfxTransform.position, _activateRadius, _whatCanSee);

            if (hit.Length == 0)
            {
                return;
            }

            if (hit[0].tag == "Player")
            {
                Vector3 povDir = hit[0].transform.position - _enemyGfxTransform.position;

                if (Vector3.Angle(povDir, _enemyGfxTransform.forward) <= _activateAngle / 2)
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
                if (_enemyConfig.ExplodeAfterDead)
                {
                    GetComponentInChildren<Explode>().ExplodeBehaviour();
                }

                Dead();
            }
        }

        private void Dead()
        {
            Destroy(this.gameObject);
        }

#if UNITY_EDITOR
        private void DrawPOVCone()
        {
            Gizmos.color = Color.green;
            Quaternion upRayRotation = Quaternion.AngleAxis(-_activateAngle / 2 + 270, Vector3.up);
            Quaternion downRayRotation = Quaternion.AngleAxis(_activateAngle / 2 + 270, Vector3.up);

            Vector3 upRayDirection = upRayRotation * transform.right * _activateRadius;
            Vector3 downRayDirection = downRayRotation * transform.right * _activateRadius;

            Gizmos.DrawRay(transform.position, upRayDirection);
            Gizmos.DrawRay(transform.position, downRayDirection);
            Gizmos.DrawLine(transform.position + downRayDirection, transform.position + transform.forward * _activateRadius);
            Gizmos.DrawLine(transform.position + upRayDirection, transform.position + transform.forward * _activateRadius);
        }

        private void OnDrawGizmos()
        {
            DrawPOVCone();
        }
#endif
    }
}

