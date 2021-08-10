using System.Collections.Generic;
using Raven.Config;
using Raven.Core.Interface;
using Raven.Player;
using UnityEngine;
using Zenject;

namespace Raven.Container
{
    [CreateAssetMenu(fileName = "Player States Container", menuName = "Containers/Player/Player States", order = 0)]
    public class PlayerStatesContainer : ScriptableObject
    {
        [SerializeField] private PlayerStateConfig[] _configs;

        public PlayerStateConfig FindStateConfig(PlayerStateName p_playerStateName)
        {
            for (int i = 0; i < _configs.Length; i++)
            {
                if (_configs[i].PlayerStateName == p_playerStateName)
                {
                    return _configs[i];
                }
            }

            return null;
        }
    }
}

