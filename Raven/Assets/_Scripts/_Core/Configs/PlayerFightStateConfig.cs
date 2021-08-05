using NaughtyAttributes;
using UnityEngine;

namespace Raven.Config
{
    public enum PlayerStateName { Normal, Fire, Ghost, Ice }

    [CreateAssetMenu(fileName = "Player Fight State Config", menuName = "Configs/Player/Player Fight State", order = 2)]
    public class PlayerFightStateConfig : ScriptableObject
    {
        [SerializeField] private PlayerStateName _playerStateName;
        [SerializeField] private bool _bulletSubtractEnergy;
        [SerializeField] private GameObject _bulletPrefab;
        [ShowIf("_bulletSubtractEnergy"), SerializeField, Range(0, 100)] private float _bulletCost;
        [SerializeField, Range(0, 100)] private float _bulletsPower;
        [SerializeField, Range(0, 100)] private float _bulletsSpeed;
        [SerializeField, Range(0, 100)] private float _dashCost;

        public PlayerStateName PlayerStateName => _playerStateName;
        public float BulletCost => _bulletCost;
        public GameObject BulletPrefab => _bulletPrefab;
        public float BulletPower => _bulletsSpeed;
        public float BulletSpeed => _bulletsSpeed;
        public float DashCost => _dashCost;
    }
}

