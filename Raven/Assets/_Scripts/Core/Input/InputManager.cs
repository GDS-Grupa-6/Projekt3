using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Input
{
    public class InputManager : MonoBehaviour
    {
        private Controls _controls;

        public bool CanInput;

        private void Awake()
        {
            _controls = new Controls();
        }

        private void OnEnable()
        {
            _controls.Enable();
        }

        private void OnDisable()
        {
            _controls.Disable();
        }

        public bool EscTrigerred()
        {
            return _controls.Player.ESC.triggered;
        }

        public Vector2 GetMovementAxis()
        {
            if (!CanInput)
            {
                return Vector2.zero;
            }

            return _controls.Player.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            if (!CanInput)
            {
                return Vector2.zero;
            }

            return _controls.Player.CameraLook.ReadValue<Vector2>();
        }

        public bool DashButtonPressed()
        {
            if (!CanInput)
            {
                return false;
            }

            return _controls.Player.Dash.triggered;
        }

        public bool AimButtonHold()
        {
            if (!CanInput)
            {
                return false;
            }

            float x = _controls.Player.Aim.ReadValue<float>();

            return x == 1;
        }

        public bool ActiveStateButtonPressed()
        {
            if (!CanInput)
            {
                return false;
            }

            return _controls.Player.ActiveState.triggered;
        }

        public bool DashButtonHold()
        {
            if (!CanInput)
            {
                return false;
            }

            float x = _controls.Player.DashHold.ReadValue<float>();

            return x == 1;
        }

        public bool ShootButtonPressed()
        {
            if (!CanInput)
            {
                return false;
            }

            return _controls.Player.Shoot.triggered;
        }

        public bool TakeButtonPressed()
        {
            if (!CanInput)
            {
                return false;
            }

            return _controls.Player.Take.triggered;
        }
    }
}

