using Cinemachine;
using Raven.Config;
using Raven.Container;
using Raven.Manager;
using Raven.Player;
using Raven.UI;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Zenject;

namespace Raven.Core.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("-----References-----")]
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _mainCameraTransform;
        [SerializeField] private Animator _playerAnimator;
        [SerializeField] private GameObject _shootCamera;
        [SerializeField] private CinemachineFreeLook _tppCamera;
        [SerializeField] private Slider _playerEnergySlider;
        [SerializeField] private Slider _playerLifeSlider;
        [SerializeField] private Image _viewFinder;
        [SerializeField] private Rig _playerRig;
        [SerializeField] private GameObject _rigTarget;
        [SerializeField] private GameObject _shootCameraLock;
        [SerializeField] private Transform _oneHandShootPoint;
        [SerializeField] private Transform _twoHandsShootPoint;

        [Header("-----Configs-----")]
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private PlayerDataConfig _playerDataConfig;

        [Header("-----containers-----")]
        [SerializeField] private PlayerStatesContainer _playerStatesContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStatesManager>().AsSingle().WithArguments(_playerStatesContainer, _player, _oneHandShootPoint, _twoHandsShootPoint).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHudManager>().AsSingle().WithArguments(_playerEnergySlider, _playerLifeSlider, _viewFinder, _playerDataConfig).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player, _movementConfig, _mainCameraTransform).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimatorManager>().AsSingle().WithArguments(_playerAnimator).NonLazy();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle().WithArguments(_shootCamera, _tppCamera, _player, _mainCameraTransform, _shootCameraLock).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerRigManager>().AsSingle().WithArguments(_playerRig, _rigTarget).NonLazy();
        }
    }
}
