using NaughtyAttributes;
using UnityEngine;

namespace Raven.Config
{
    public enum EnemyType { Kamikaze, Shooter }

    [CreateAssetMenu(fileName = "Enemy Config", menuName = "Configs/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [SerializeField, BoxGroup("----Basic Stats----")] private EnemyType _enemyType;
        [Space]
        [SerializeField, BoxGroup("----Basic Stats----")] private float _maxHealth = 5f;
        [SerializeField, BoxGroup("----Basic Stats----")] private float _power = 5f;
        [Space]
        [SerializeField, BoxGroup("----Basic Stats----")] private bool _explodeAfterDead;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _moveSpeed;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _maxDodgeWaitTime = 3f;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _chargeSpeedModifier = 5f;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _chargeDistance = 8f;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _explodeRadius = 3f;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _chargeTime = 1f;
        [ShowIf("_enemyType", Config.EnemyType.Kamikaze), BoxGroup("----Kamikaze Stats----")] [SerializeField] private float _chargeWaitTime = 3f;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Shooter), BoxGroup("----Shooter Stats----")] [SerializeField] private GameObject _bullet;
        [ShowIf("_enemyType", Config.EnemyType.Shooter), BoxGroup("----Shooter Stats----")] [SerializeField] private float _bulletSpeed = 10;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Shooter), BoxGroup("----Shooter Stats----")] [SerializeField] private float _shootDistance = 8f;
        [ShowIf("_enemyType", Config.EnemyType.Shooter), BoxGroup("----Shooter Stats----")] [SerializeField] private float _shootDelayTime = 2f;
        [Space]
        [ShowIf("_enemyType", Config.EnemyType.Shooter), BoxGroup("----Shooter Stats----")] [SerializeField] private bool _isStatic;

        public float MaxHealth => _maxHealth;
        public float ChargeDistance => _chargeDistance;
        public float ChargeTime => _chargeTime;
        public float ChargeWaitTime => _chargeWaitTime;
        public float Power => _power;
        public float ChargeSpeedModifier => _chargeSpeedModifier;
        public EnemyType EnemyType => _enemyType;
        public float MoveSpeed => _moveSpeed;
        public float MaxDodgeWaitTime => _maxDodgeWaitTime;
        public float ExplodeRadius => _explodeRadius;
        public bool ExplodeAfterDead => _explodeAfterDead;
        public float ShootDistance => _shootDistance;
        public bool IsStatic => _isStatic;
        public GameObject Bullet => _bullet;
        public float ShootDelayTime => _shootDelayTime;
        public float BulletSpeed => _bulletSpeed;
    }
}

