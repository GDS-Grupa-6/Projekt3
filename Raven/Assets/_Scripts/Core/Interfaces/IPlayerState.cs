using Raven.Config;
using Raven.Input;
using Raven.Manager;
using Raven.UI;

namespace Raven.Core.Interface
{
    public interface IPlayerState
    {
        void Initialize(InputController p_inputController, PlayerHudManager p_hudManager,
            PlayerStatesManager p_playerStatesManager);
        void ActiveDash(PlayerMovementManager p_movementManager);
        void Dash(PlayerMovementManager p_movementManager);
    }
}

