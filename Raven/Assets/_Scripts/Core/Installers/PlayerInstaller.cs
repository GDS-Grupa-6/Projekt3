using Cinemachine;
using Raven.Config;
using Raven.Container;
using Raven.Manager;
using Raven.Player;
using Raven.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Zenject;

namespace Raven.Core.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _shootRaycastHits;
        [Header("-----References-----")]
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _mainCameraTransform;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GameObject _shootCamera;
        [SerializeField] private CinemachineFreeLook _tppCamera;
        [SerializeField] private Rig[] _playerRigs;
        [SerializeField] private GameObject _rigTarget;
        [SerializeField] private GameObject _shootCameraLock;
        [SerializeField] private Transform _oneHandShootPoint;
        [SerializeField] private Transform _twoHandsShootPoint;
        [SerializeField] private GameObject _secondWeapon;
        [SerializeField] private PlayerHudReferences _hudReferences;
        [SerializeField] private Player.Collectible[] _collectibles;
        [SerializeField] private Transform _playerGroundCheck;

        [Header("-----Configs-----")]
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private PlayerDataConfig _playerDataConfig;

        [Header("-----containers-----")]
        [SerializeField] private PlayerStatesContainer _playerStatesContainer;

        public override void InstallBindings()
        {
            Container.Bind<PlayerDataManager>().AsSingle().WithArguments(_playerDataConfig);
            Container.BindInterfacesAndSelfTo<PlayerStatesManager>().AsSingle().WithArguments(_playerStatesContainer, _player, _oneHandShootPoint, _twoHandsShootPoint, _secondWeapon).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHudManager>().AsSingle().WithArguments(_hudReferences, _playerDataConfig, _collectibles).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player, _movementConfig, _mainCameraTransform, _playerGroundCheck).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimatorManager>().AsSingle().WithArguments(_playerAnimator).NonLazy();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle().WithArguments(_shootCamera, _tppCamera, _player, _mainCameraTransform, _shootCameraLock, _movementConfig).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerRigManager>().AsSingle().WithArguments(_playerRigs, _rigTarget, _shootRaycastHits).NonLazy();
        }
    }
}
