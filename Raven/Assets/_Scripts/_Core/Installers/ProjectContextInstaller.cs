using Raven.Manager;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [Header("-----References-----")]
    [SerializeField] private CoroutinesManager _coroutinesManager;

    public override void InstallBindings()
    {
        Container.Bind<CoroutinesManager>().FromInstance(_coroutinesManager).AsSingle().NonLazy();
    }
}