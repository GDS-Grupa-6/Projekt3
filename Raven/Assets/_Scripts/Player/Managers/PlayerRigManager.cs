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

        private Rig[] _rigs;
        private GameObject _rigTarget;
        private LayerMask _rayLayerMask;

        private bool _activeWeight;

        public GameObject RigTarget => _rigTarget;

        public bool SecondWeapon;

        public PlayerRigManager(CameraManager p_cameraManager, Rig[] p_rigs, GameObject p_rigTarget, LayerMask p_rayMask)
        {
            _rayLayerMask = p_rayMask;
            _rigs = p_rigs;
            _rigTarget = p_rigTarget;
            _cameraManager = p_cameraManager;

            foreach (var rig in _rigs)
            {
                rig.weight = 0;
            }

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
                if (SecondWeapon)
                {
                    foreach (var rig in _rigs)
                    {
                        rig.weight = 1;
                    }
                }
                else
                {
                    _rigs[0].weight = 1;
                }
            }
            else
            {
                foreach (var rig in _rigs)
                {
                    rig.weight = 0;
                }
            }

            _rigTarget.transform.position = GetRaycastHit().point;
        }

        public RaycastHit GetRaycastHit()
        {
            RaycastHit hit;
            Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit,9999, _rayLayerMask);
            return hit;
        }

        private void ActiveWeight(bool p_aim)
        {
            _activeWeight = p_aim;
        }
    }
}

