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
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
        Idle = 4,
    }
    
    public class ChickenSprite
    {
        private KeyboardState keyboardState;

        private Texture2D texture;

        private bool inMotion;

        private Vector2 position = new Vector2(100, 100);

        private ChickenDirection chickenDirection;

        private InputManager inputManager = new InputManager();
        private BoundingRectangle bounds = new BoundingRectangle(new Vector2(200 - 16, 200 - 16), 32, 32);

        public Color color;


        public BoundingRectangle Bounds => bounds;

        //private double directionTimer;
        private double animationTimer;
        private short animationFrame;

        


        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Chicken");
        }


        public void Update(GameTime gameTime)
        {
            

            #region keyboard inputs

            keyboardState = Keyboard.GetState();
            if (keyboardState.IsKeyDown(Keys.W))
            {
                position += new Vector2(0, -100 *(float)gameTime.ElapsedGameTime.TotalSeconds);
                chickenDirection = ChickenDirection.Up;
            }
            if(keyboardState.IsKeyDown(Keys.S))
            {
                position += new Vector2(0, 100 * (float)gameTime.ElapsedGameTime.TotalSeconds);
                chickenDirection = ChickenDirection.Down;
            }
            if (keyboardState.IsKeyDown(Keys.A))
            {
                position += new Vector2(-100 *(float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                chickenDirection = ChickenDirection.Left;
            }
            if (keyboardState.IsKeyDown(Keys.D))
            {
                position += new Vector2(100 * (float)gameTime.ElapsedGameTime.TotalSeconds, 0);
                chickenDirection = ChickenDirection.Right;
            }

            #endregion

            bounds.X = position.X - 16;
            bounds.Y = position.Y - 16;
        }


        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            var source = new Rectangle(0, 0, 32, 32);
            //check to see if the chicken is in motion
            inMotion = inputManager.MotionCheck();
            if (inMotion)
            {
                animationTimer += gameTime.ElapsedGameTime.TotalSeconds;
                if(animationTimer > 0.3)
                {
                    animationFrame += 2;
                    if(animationFrame > 2)
                    {
                        animationFrame = 0;
                    }
                    animationTimer -= 0.3;
                }
                source = new Rectangle(animationFrame * 32, (int)chickenDirection * 32, 32, 32);
            }
            else
            {
                source = new Rectangle(32, (int)chickenDirection * 32, 32, 32);
            }
            

            
            //spriteBatch.Draw(texture, position, source, Color.White);
            spriteBatch.Draw(texture, position, source, Color.White, 0, new Vector2(0, 0), 1.75f, SpriteEffects.None, 0);
        }
    }
}
