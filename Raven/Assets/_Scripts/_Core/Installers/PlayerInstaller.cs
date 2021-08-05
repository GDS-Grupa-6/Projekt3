using Cinemachine;
using Raven.Config;
using Raven.Container;
using Raven.Manager;
using Raven.UI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Raven.Core
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

        [Header("-----Configs-----")]
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private PlayerDataConfig _playerDataConfig;

        [Header("-----containers-----")]
        [SerializeField] private PlayerStatesContainer _playerStatesContainer;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerStatesManager>().AsSingle().WithArguments(_playerStatesContainer).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHudManager>().AsSingle().WithArguments(_playerEnergySlider, _playerLifeSlider, _playerDataConfig).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player, _movementConfig, _mainCameraTransform).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimatorManager>().AsSingle().WithArguments(_playerAnimator).NonLazy();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle().WithArguments(_shootCamera, _tppCamera, _player, _mainCameraTransform).NonLazy();
        }
    }
}
