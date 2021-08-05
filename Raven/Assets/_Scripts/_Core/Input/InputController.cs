using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Input
{
    public class InputController : MonoBehaviour
    {
        private Controls _controls;

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

        public Vector2 GetMovementAxis()
        {
            return _controls.Player.Movement.ReadValue<Vector2>();
        }

        public Vector2 GetMouseDelta()
        {
            return _controls.Player.CameraLook.ReadValue<Vector2>();
        }

        public bool DashButtonPressed()
        {
            return _controls.Player.Dash.triggered;
        }

        public bool AimButtonPressed()
        {
            float x = _controls.Player.Aim.ReadValue<float>();

            return x == 1;
        }

        public bool State1ButtonPressed()
        {
            return _controls.Player.State1.triggered;
        }

        public bool State2ButtonPressed()
        {
            return _controls.Player.State2.triggered;
        }

        public bool State3ButtonPressed()
        {
            return _controls.Player.State3.triggered;
        }

        public bool State4ButtonPressed()
        {
            return _controls.Player.State4.triggered;
        }
    }
}

