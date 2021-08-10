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

        private bool _activeWeight;
        private float _lerpTimer;

        public PlayerRigManager(CameraManager p_cameraManager, Rig p_rig, GameObject p_rigTarget)
        {
            _rig = p_rig;
            _rigTarget = p_rigTarget;
            _cameraManager = p_cameraManager;

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
                WeightLerp(1f);
            }
            else
            {
                WeightLerp(0f);
            }
        }

        private void ActiveWeight(bool p_aim)
        {
            _activeWeight = p_aim;
        }

        private void WeightLerp(float p_value)
        {
            if (!Mathf.Approximately(_rig.weight, p_value))
            {
                _lerpTimer = Time.deltaTime * 10f;
                _rig.weight = Mathf.Lerp(_rig.weight, p_value, _lerpTimer);
            }
            else
            {
                _lerpTimer = 0;
            }
        }
    }
}

