using NaughtyAttributes;
using Raven.Config;
using Raven.Manager;
using Raven.Player;
using UnityEngine;
using Zenject;

namespace Raven.Enemy
{
    public class Explode : MonoBehaviour
    {
        [SerializeField] private GameObject _effectPrefab;
        [HideIf("_effectOnEnemy"), SerializeField] private float _power = 2;
        [HideIf("_effectOnEnemy"), SerializeField] private float _explodeRadius = 5;
        [SerializeField] private Transform _effectPosition;
        [SerializeField] private bool _effectOnEnemy;
        [ShowIf("_effectOnEnemy"), SerializeField] private EnemyConfig _config;

        private PlayerDataManager _playerDataManager;
        private float _currentPower;
        private Vector3 _currentEffectPosition;
        private float _currentExplodeRadius;

        [Inject]
        public void Construct(PlayerDataManager p_playerDataManager)
        {
            _playerDataManager = p_playerDataManager;

            if (_effectOnEnemy)
            {
                _currentPower = _config.Power;
                _currentExplodeRadius = _config.ExplodeRadius;
            }
            else
            {
                _currentEffectPosition = _effectPosition.position;
                _currentPower = _power;
                _currentExplodeRadius = _explodeRadius;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_effectOnEnemy && other.tag == "Player")
            {
                GetComponentInParent<EnemyController>().TakeDamage(_config.MaxHealth);
            }
        }

        public void ExplodeBehaviour()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _currentExplodeRadius, transform.forward, _currentExplodeRadius);

            var obj = Instantiate(_effectPrefab);
            obj.transform.position = _effectPosition.position;

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Enemy" && hits[i].collider.gameObject != this.gameObject)
                {
                    hits[i].collider.gameObject.GetComponent<EnemyController>().TakeDamage(_currentPower);
                }
                else if (hits[i].collider.tag == "Player")
                {
                    if (_effectOnEnemy) _currentEffectPosition = transform.position;

                    _playerDataManager.TakeDamage(_currentPower);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireSphere(transform.position, _effectOnEnemy ? _config.ExplodeRadius : _explodeRadius);
        }
#endif
    }
}

