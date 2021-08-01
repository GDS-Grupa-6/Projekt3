using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Config
{
    [CreateAssetMenu(fileName = "Movement Config", menuName = "Configs/Player/Movement Config", order = 0)]
    public class MovementConfig : ScriptableObject
    {
        [SerializeField, Range(1, 5)] private float _moveSpeed = 3f;
        [SerializeField, Range(0.1f, 1)] private float _turnSmoothTime = 0.1f;

        public float MoveSpeed => _moveSpeed;
        public float TurnSmoothTime => _turnSmoothTime;
    }
}

