using Raven.Player;
using UnityEngine;
using Zenject;

namespace Raven.Core.Installer
{
    public class PlayerStatesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<NormalState>().AsSingle().NonLazy();
            Container.Bind<FireState>().AsSingle().NonLazy();
            Container.Bind<GhostState>().AsSingle().NonLazy();
            Container.Bind<IceState>().AsSingle().NonLazy();
        }
    }
}
