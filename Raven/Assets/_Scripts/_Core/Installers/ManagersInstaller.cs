using Cinemachine;
using Raven.Config;
using Raven.Input;
using Raven.Manager;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Raven.Core
{
    public class ManagersInstaller : MonoInstaller
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

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player, _movementConfig, _mainCameraTransform).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimatorManager>().AsSingle().WithArguments(_playerAnimator).NonLazy();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle().WithArguments(_shootCamera, _tppCamera, _player, _mainCameraTransform).NonLazy();
        }
    }
}
