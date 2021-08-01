using Raven.Config;
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

        [Header("-----Configs-----")]
        [SerializeField] private MovementConfig _movementConfig;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player, _movementConfig, _mainCameraTransform).NonLazy();
        }
    }
}
