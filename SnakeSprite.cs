using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public enum SnakeDirection
    {
        Right = 0,
        Left = 1,
    }

    public class SnakeSprite
    {
        private Texture2D texture;
        private double directionTimer;
        private double animationTimer;
        private short animationFrame;
        private bool flipped;

        public SnakeDirection snakeDirection;
        public Vector2 Position;

        public void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Snake");
        }

        public void Update(GameTime gameTime)
        {
            directionTimer += gameTime.ElapsedGameTime.TotalSeconds;

            //switch the direction every two seconds
            if(directionTimer > 2.0)
            {
                if(snakeDirection == SnakeDirection.Left)
                {
                    snakeDirection = SnakeDirection.Right;
                    //Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                    flipped = false;
                }
                else
                {
                    snakeDirection = SnakeDirection.Left;
                    flipped = true;
                }
                directionTimer -= 2.0;
            }

            if (snakeDirection == SnakeDirection.Left)
            {
                Position += new Vector2(-1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                Position += new Vector2(1, 0) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
        }

        public void Draw(GameTime gametime, SpriteBatch spriteBatch)
        {
            animationTimer += gametime.ElapsedGameTime.TotalSeconds;
            if(animationTimer > 0.5)
            {
                animationFrame++;
                if(animationFrame > 1)
                {
                    animationFrame = 0;
                }
                animationTimer -= 0.5;
            }
            var source = new Rectangle(animationFrame * 40, (int)snakeDirection * 29, 40, 29);
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, Position, source, Color.White);
            //spriteBatch.Draw
        }
    }
}
