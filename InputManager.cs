using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class InputManager
    {
        KeyboardState currentKeyboard;
        KeyboardState priorKeyboard;

        public Vector2 Direction { get; private set; }

        public bool ExitState { get; private set; }

        public void Update(GameTime gameTime)
        {

        }
    }
}
