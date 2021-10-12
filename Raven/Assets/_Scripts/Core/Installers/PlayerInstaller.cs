using Cinemachine;
using Raven.Config;
using Raven.Container;
using Raven.Manager;
using Raven.Player;
using Raven.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;
using Zenject;

namespace Raven.Core.Installer
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private LayerMask _shootRaycastHits;
        [Header("-----References-----")]
        [SerializeField] private PlayerReferences _playerReferences;
        [SerializeField] private Transform _mainCameraTransform;   
        [SerializeField] private GameObject _shootCamera;
        [SerializeField] private CinemachineFreeLook _tppCamera;    
        [SerializeField] private GameObject _rigTarget;
        [SerializeField] private GameObject _shootCameraLock;      
        [SerializeField] private PlayerHudReferences _hudReferences;
        [SerializeField] private Player.Collectible[] _collectibles;
        [SerializeField] private Animator _deadPanelAnimator;

        [Header("-----Configs-----")]
        [SerializeField] private MovementConfig _movementConfig;
        [SerializeField] private PlayerDataConfig _playerDataConfig;

        [Header("-----containers-----")]
        [SerializeField] private PlayerStatesContainer _playerStatesContainer;

        public override void InstallBindings()
        {
            Container.Bind<PlayerDataManager>().AsSingle().WithArguments(_playerDataConfig, _deadPanelAnimator);
            Container.BindInterfacesAndSelfTo<PlayerStatesManager>().AsSingle().WithArguments(_playerStatesContainer, _playerReferences).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerHudManager>().AsSingle().WithArguments(_hudReferences, _playerDataConfig, _collectibles).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerMovementManager>().AsSingle().WithArguments(_playerReferences.Player, _movementConfig, _mainCameraTransform, _playerReferences.PlayerGroundCheck).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerAnimatorManager>().AsSingle().WithArguments(_playerReferences.PlayerAnimator).NonLazy();
            Container.BindInterfacesAndSelfTo<CameraManager>().AsSingle().WithArguments(_shootCamera, _tppCamera, _playerReferences.Player, _mainCameraTransform, _shootCameraLock, _movementConfig).NonLazy();
            Container.BindInterfacesAndSelfTo<PlayerRigManager>().AsSingle().WithArguments(_playerReferences.PlayerRigs, _rigTarget, _shootRaycastHits).NonLazy();
        }
    }
}
