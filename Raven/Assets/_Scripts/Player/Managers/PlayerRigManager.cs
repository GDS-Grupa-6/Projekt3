using System;
using Raven.Manager;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using Zenject;

namespace Raven.Player
{
    public class PlayerRigManager : ITickable, IDisposable
    {
        private CameraManager _cameraManager;

        private Rig _rig;
        private GameObject _rigTarget;
        private GameObject _cameraLock;

        private bool _activeWeight;

        public GameObject RigTarget => _rigTarget;

        public PlayerRigManager(CameraManager p_cameraManager, Rig p_rig, GameObject p_rigTarget)
        {
            _rig = p_rig;
            _rigTarget = p_rigTarget;
            _cameraManager = p_cameraManager;
            _cameraLock = _cameraManager.ShootCameraLock;

            _rig.weight = 0;

            _cameraManager.OnAimChange += ActiveWeight;
        }

        public void Dispose()
        {
            _cameraManager.OnAimChange -= ActiveWeight;
        }

        public void Tick()
        {
            if (_activeWeight)
            {
                _rig.weight = 1;
            }
            else
            {
                _rig.weight = 0;
            }

            _rigTarget.transform.position = GetRaycastHitPosition();
        }

        public Vector3 GetRaycastHitPosition()
        {
            RaycastHit hit;
            Physics.Raycast(_cameraLock.transform.position, _cameraLock.transform.forward, out hit);

            return hit.point;
        }

        private void ActiveWeight(bool p_aim)
        {
            _activeWeight = p_aim;
        }
    }
}

