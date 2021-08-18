using NaughtyAttributes;
using Raven.Config;
using Raven.Manager;
using Raven.Player;
using UnityEngine;
using Zenject;

namespace Raven.Enemy
{
    public class EnemyExplode : MonoBehaviour
    {
        [InfoBox("Set this component on GFX object of enemy")]
        [SerializeField] private EnemyConfig _config;
        [SerializeField] private GameObject _effectPrefab;

        private PlayerDataManager _playerDataManager;

        [Inject]
        public void Construct(PlayerDataManager p_playerDataManager)
        {
            _playerDataManager = p_playerDataManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                GetComponentInParent<EnemyController>().TakeDamage(_config.MaxHealth);
            }
        }

        public void Explode()
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _config.ExplodeRadius, transform.forward);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.tag == "Enemy" && hits[i].collider.gameObject != this.gameObject)
                {
                    Debug.Log($"Hit: {hits[i].collider.gameObject.name}");
                }
                else if (hits[i].collider.tag == "Player")
                {
                    var obj = Instantiate(_effectPrefab);
                    obj.transform.position = transform.position;

                    _playerDataManager.TakeDamage(_config.Power);
                }
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _config.ExplodeRadius);
        }
#endif
    }
}

