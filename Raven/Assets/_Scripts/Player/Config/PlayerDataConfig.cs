using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Raven.Config
{
    [CreateAssetMenu(fileName = "Player Data Config", menuName = "Configs/Player/Data Config", order = 1)]
    public class PlayerDataConfig : ScriptableObject
    {
        [Header("-----ENERGY CONFIG-----")]
        [SerializeField, Range(1f, 200f)] private float _maxEnergyValue = 50f;
        [SerializeField, Range(0.1f, 5f)] private float _regenerationTime = 2f;
        [SerializeField, Range(0.1f, 5f)] private float _timeToStartRegeneration = 2;
        [SerializeField, Range(1f, 100f)] private int _regenerationValue = 2;
        [Header("-----HEALTH CONFIG-----")]
        [SerializeField, Range(1f, 100f)] private float _maxHealthValue = 100f;

        public int RegenerationValue => _regenerationValue;
        public float TimeToStartRegeneration => _timeToStartRegeneration;
        public float MaxEnergyValue => _maxEnergyValue;
        public float RegenerationTime => _regenerationTime;
        public float MaxHealthValue => _maxHealthValue;
    }
}

