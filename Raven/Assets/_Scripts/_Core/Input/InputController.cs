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
    }
}

