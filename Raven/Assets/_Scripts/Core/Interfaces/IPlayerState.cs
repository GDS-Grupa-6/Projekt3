using Raven.Config;
using Raven.Input;
using Raven.Manager;
using Raven.Player;
using Raven.UI;
using UnityEngine;

namespace Raven.Core.Interface
{
    public interface IPlayerState
    {
        void Initialize(InputManager pInputManager, PlayerHudManager p_hudManager,
            PlayerStatesManager p_playerStatesManager);
        void ActiveDash(PlayerMovementManager p_movementManager);
        void Dash(PlayerMovementManager p_movementManager);
        void Shoot(Transform p_shootPoint, Transform p_lookAt);
    }
}

