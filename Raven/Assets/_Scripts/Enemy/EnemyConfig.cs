using NaughtyAttributes;
using UnityEngine;

namespace Raven.Config
{
    public enum EnemyType { Kamikaze, Shooter }

    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField] private EnemyType _enemyType;
        [SerializeField] private float _maxHealth = 5f;
        [SerializeField] private float _power = 5f;

        [ShowIf("_enemyType", Config.EnemyType.Kamikaze)] [SerializeField] private float _moveSpeed;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze)] [SerializeField] private float _maxDodgeWaitTime = 3f;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze)] [SerializeField] private float _speedModifier = 5f;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze)] [SerializeField] private float _explodeRadius = 3f;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze)] [SerializeField] private float _chargeTime = 1f;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze)] [SerializeField] private float _chargeWaitTime = 3f;

        public float MaxHealth => _maxHealth;
        public float ChargeTime => _chargeTime;
        public float ChargeWaitTime => _chargeWaitTime;
        public float Power => _power;
        public float SpeedModifier => _speedModifier;
        public EnemyType EnemyType => _enemyType;
        public float MoveSpeed => _moveSpeed;
        public float MaxDodgeWaitTime => _maxDodgeWaitTime;
        public float ExplodeRadius => _explodeRadius;
    }
}

