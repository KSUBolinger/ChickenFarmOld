using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace GameProject0
{
    public enum ChickenDirection
    {
        Down = 0,
        Right = 1,
        Up = 2,
        Left = 3,
        Idle = 4,
    }
    public class ChickenSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private bool flipped;

        private Vector2 position = new Vector2(200, 200);

        private ChickenDirection chickenDirection;

        private double directionTimer;
        private short animationFrame;

        


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Chicken");
        }


        public void Update(GameTime gameTime)
        {
            keyboardState = Keyboard.GetState();

            //keyboard inputs 
            if(keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -1);
                chickenDirection = ChickenDirection.Up;
            }
            if(keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 1);
                chickenDirection = ChickenDirection.Down;
            }
            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-1, 0);
                chickenDirection = ChickenDirection.Left;
            }

            if (keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(1, 0);
                chickenDirection = ChickenDirection.Right;
            }
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {

            var source = new Rectangle(32, 64, 32, 32);
            spriteBatch.Draw(texture, position, source, Color.White);
        }
    }
}
