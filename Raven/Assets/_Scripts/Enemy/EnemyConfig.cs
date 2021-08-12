using UnityEngine;

namespace Raven.Config
{
    public enum EnemyType { Kamikaze, Shooter }

    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _moveSpeed;

        public EnemyType EnemyType => _enemyType;
        public float MoveSpeed => _moveSpeed;
    }
}

