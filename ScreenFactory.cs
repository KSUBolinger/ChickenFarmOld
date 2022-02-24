/*
    This screen is an adapted version of the one provided by Nathan Bean in the Game Architecture Tutorial to suit this game
*/
using System;
using System.Collections.Generic;
using System.Text;
using GameProject0.StateManagement;

namespace GameProject0
{
    public class ScreenFactory : IScreenFactory
    {
        public GameScreen CreateScreen(Type screenType)
        {
            // All of our screens have empty constructors so we can just use Activator
            return Activator.CreateInstance(screenType) as GameScreen;
        }
    }
}
