using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Config
{
    [CreateAssetMenu(fileName = "Player Data Config", menuName = "Configs/Player/Data Config", order = 1)]
    public class PlayerDataConfig : ScriptableObject
    {
        [SerializeField, Range(1f, 200f)] private float _maxEnergyValue = 50f;
        [SerializeField, Range(1f, 100f)] private float _maxHealthValue = 100f;

        public float MaxEnergyValue => _maxEnergyValue;
        public float MaxHealthValue => _maxHealthValue;
    }
}

