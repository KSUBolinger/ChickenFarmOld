/*
    This state is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;

namespace GameProject0.StateManagement
{
    /// <summary>
    /// Defines an object that can create a screen when given its type.
    /// </summary>
    public interface IScreenFactory
    {
        GameScreen CreateScreen(Type screenType);
    }
}
