using Raven.Config;
using Raven.Player;
using UnityEngine;
using Zenject;

namespace Raven.Enemy
{
    public class EnemyExplode : MonoBehaviour
    {
        [HideInInspector] public GameObject Mother;

        private PlayerDataManager _playerDataManager;
        private EnemyConfig _config;

        public void Initialization(EnemyConfig p_config, PlayerDataManager p_playerDataManager)
        {
            _config = p_config;
            _playerDataManager = p_playerDataManager;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                RaycastHit[] hits = Physics.SphereCastAll(transform.position, _config.ExplodeRadius, transform.forward);

                _playerDataManager.TakeDamage(_config.Power);

                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.tag == "Enemy" && hits[i].collider.gameObject != this.gameObject)
                    {
                        Debug.Log($"Hit: {hits[i].collider.gameObject.name}");
                    }
                }

                Destroy(Mother);
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

