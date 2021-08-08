using NaughtyAttributes;
using Raven.Core.Interface;
using UnityEngine;

namespace Raven.Config
{
    public enum PlayerStateName { Normal, Fire, Ghost, Ice }

    [CreateAssetMenu(fileName = "Player Fight State Config", menuName = "Configs/Player/Player Fight State", order = 2)]
    public class PlayerFightStateConfig : ScriptableObject
    {
        [SerializeField] private PlayerStateName _playerStateName;
        [Space]
        [SerializeField] private bool _bulletSubtractEnergy;
        [SerializeField] private GameObject _bulletPrefab;
        [ShowIf("_bulletSubtractEnergy"), SerializeField, Range(0, 100)] private float _bulletCost;
        [SerializeField, Range(0, 100)] private float _bulletsPower;
        [SerializeField, Range(0, 100)] private float _bulletsSpeed;
        [Space]
        [SerializeField] private bool _dashHold;
        [SerializeField] private bool _dashHasEffect;
        [SerializeField, Range(1, 150)] private float _dashSpeed = 100f;
        [HideIf("_dashHold"), SerializeField, Range(0.1f, 0.5f)] private float _dashTime = 0.1f;
        [SerializeField, Range(0, 100)] private float _dashCost;
        [ShowIf("_dashHasEffect"), SerializeField] private GameObject _effectPrefab;
        [ShowIf("_dashHasEffect"), SerializeField] private float _effectRadius = 4f;
        [ShowIf("_dashHasEffect"), SerializeField] private float _effectPower = 4f;
        [ShowIf("_dashHasEffect"), SerializeField] private float _effectTime = 1f;

        public float EffectRadius => _effectRadius;
        public float EffectTime => _effectTime;
        public GameObject EffectPrefab => _effectPrefab;
        public float EffectPower => _effectPower;
        public IPlayerState PlayerStateBehaviour;
        public PlayerStateName PlayerStateName => _playerStateName;
        public float BulletCost => _bulletCost;
        public GameObject BulletPrefab => _bulletPrefab;
        public float BulletPower => _bulletsSpeed;
        public float BulletSpeed => _bulletsSpeed;
        public float DashCost => _dashCost;
        public float DashSpeed => _dashSpeed;
        public float DashTime => _dashTime;
        public bool DashHold => _dashHold;
    }
}

