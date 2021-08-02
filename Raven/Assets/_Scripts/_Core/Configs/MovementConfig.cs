using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Config
{
    [CreateAssetMenu(fileName = "Movement Config", menuName = "Configs/Player/Movement Config", order = 0)]
    public class MovementConfig : ScriptableObject
    {
        [SerializeField, Range(1, 50)] private float _moveSpeed = 10f;
        [Space]
        [SerializeField, Range(1, 150)] private float _dashSpeed = 100f;
        [SerializeField, Range(0.1f, 0.5f)] private float _dashTime = 0.1f;
        [Space]
        [SerializeField, Range(0.1f, 0.5f)] private float _turnSmoothTime = 0.1f;
        [Space]
        [SerializeField, Range(-30, -1)] private float _gravityValue = -9.81f;

        public float MoveSpeed => _moveSpeed;
        public float DashSpeed => _dashSpeed;
        public float DashTime => _dashTime;
        public float TurnSmoothTime => _turnSmoothTime;
        public float GravityValue => _gravityValue;
    }
}

