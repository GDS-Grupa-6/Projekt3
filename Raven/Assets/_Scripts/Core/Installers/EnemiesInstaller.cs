using Raven.Manager;
using UnityEngine;
using Zenject;

public class EnemiesInstaller : MonoInstaller
{
    [SerializeField] private GameObject _player;

    public override void InstallBindings()
    {
    }
}
