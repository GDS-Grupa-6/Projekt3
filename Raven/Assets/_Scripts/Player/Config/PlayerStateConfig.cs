using NaughtyAttributes;
using Raven.Core.Interface;
using UnityEngine;

namespace Raven.Config
{
    public enum PlayerStateName { Normal, Fire }

    [CreateAssetMenu(fileName = "Player State Config", menuName = "Configs/Player/Player State", order = 2)]
    public class PlayerStateConfig : ScriptableObject
    {
        [SerializeField] private PlayerStateName _playerStateName;
        [Space]
        [SerializeField] private GameObject _bulletPrefab;
        [SerializeField] private float _bulletSpeed = 5f;
        [SerializeField] private float _bulletLifeTime = 5f;
        [SerializeField] private float _bulletPower = 5f;
        [SerializeField] private float _oneHandDelay = 1f;
        [SerializeField] private float _twoHandsDelay = 0.5f;
        [Space]
        [SerializeField] private bool _dashHasEffect;
        [SerializeField, Range(1, 150)] private float _dashSpeed = 100f;
        [SerializeField, Range(0.1f, 0.5f)] private float _dashTime = 0.1f;
        [SerializeField, Range(0, 100)] private float _dashCost;
        [ShowIf("_dashHasEffect"), SerializeField] private GameObject _effectPrefab;
        [ShowIf("_dashHasEffect"), SerializeField] private float _effectRadius = 4f;
        [ShowIf("_dashHasEffect"), SerializeField] private float _effectPower = 4f;
        [ShowIf("_dashHasEffect"), SerializeField] private float _effectTime = 1f;

        public float BulletLifeTime => _bulletLifeTime;
        public float BulletSpeed => _bulletSpeed;
        public float bulletPower => _bulletPower;
        public float EffectRadius => _effectRadius;
        public float EffectTime => _effectTime;
        public GameObject EffectPrefab => _effectPrefab;
        public float EffectPower => _effectPower;
        public IPlayerState PlayerStateBehaviour;
        public PlayerStateName PlayerStateName => _playerStateName;
        public GameObject BulletPrefab => _bulletPrefab;
        public float DashCost => _dashCost;
        public float DashSpeed => _dashSpeed;
        public float DashTime => _dashTime;
        public float OneHandDelay => _oneHandDelay;
        public float TwoHandsDelay => _twoHandsDelay;
    }
}

