using Raven.Config;
using Raven.Input;
using Raven.Manager;
using UnityEngine;
using Zenject;

namespace Raven.Core
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("-----References-----")]
        [SerializeField] private GameObject _player;
        [SerializeField] private Transform _mainCameraTransform;
        [SerializeField] private Animator _playerAnimator;

        [Header("-----Configs-----")]
        [SerializeField] private MovementConfig _movementConfig;

        public override void InstallBindings()
        {
            Container.Bind<InputController>().FromComponentOn(_player).AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player, _movementConfig, _mainCameraTransform).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimatorManager>().AsSingle().WithArguments(_playerAnimator).NonLazy();
        }
    }
}
