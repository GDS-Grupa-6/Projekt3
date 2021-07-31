using Raven.Manager;
using UnityEngine;
using Zenject;

namespace Raven.Core
{
    public class PlayerInstaller : MonoInstaller
    {
        [Header("-----References-----")]
        [SerializeField] private GameObject _player;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_player).NonLazy();
        }
    }
}
