/*
    This screen is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;
using Microsoft.Xna.Framework;

namespace GameProject0.Screens
{
    // Custom event argument which includes the index of the player who
    // triggered the event. This is used by the MenuEntry.Selected event.
    public class PlayerIndexEventArgs : EventArgs
    {
        public PlayerIndex PlayerIndex { get; }

        public PlayerIndexEventArgs(PlayerIndex playerIndex)
        {
            PlayerIndex = playerIndex;
        }
    }
}
