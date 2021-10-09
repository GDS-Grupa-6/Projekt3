using UnityEngine;
using Zenject;

public class ManagersInstaller : MonoInstaller
{
    [Header("-----References-----")]
    [SerializeField] private AudioReferences _audioReferences;

    public override void InstallBindings()
    {
        Container.Bind<AudioManager>().AsSingle().WithArguments(_audioReferences).NonLazy();
    }
}