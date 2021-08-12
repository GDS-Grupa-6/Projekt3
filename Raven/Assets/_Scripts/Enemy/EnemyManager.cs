using Raven.Config;
using Raven.Enemy;
using UnityEngine;
using Zenject;

namespace Raven.Manager
{
    public class EnemyManager : MonoBehaviour
    {
        [SerializeField] private EnemyConfig _enemyConfig;
        [SerializeField] private GameObject _player;

        /*[Inject]
        public void Construct(GameObject p_player)
        {
            _player = p_player;
        }*/

        private void Awake()
        {
            switch (_enemyConfig.EnemyType)
            {
                case EnemyType.Kamikaze:
                    gameObject.AddComponent<FollowEnemyMovement>().Construct(_enemyConfig, _player);
                    break;

                case EnemyType.Shooter:
                    //TODO behaviour
                    break;
            }
        }
    }
}

