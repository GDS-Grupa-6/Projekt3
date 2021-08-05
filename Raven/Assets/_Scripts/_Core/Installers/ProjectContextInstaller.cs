using Raven.Input;
using Raven.Manager;
using UnityEngine;
using Zenject;

public class ProjectContextInstaller : MonoInstaller
{
    [Header("-----References-----")]
    [SerializeField] private CoroutinesManager _coroutinesManager;
    [SerializeField] private InputController _inputController;

    public override void InstallBindings()
    {
        Container.Bind<CoroutinesManager>().FromInstance(_coroutinesManager).AsSingle().NonLazy();
        Container.Bind<InputController>().FromInstance(_inputController).AsSingle().NonLazy();
    }
}