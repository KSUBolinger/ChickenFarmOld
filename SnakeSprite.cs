using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public enum Direction
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

        public Direction Direction;
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
                if(Direction == Direction.Left)
                {
                    Direction = Direction.Right;
                    Position += new Vector2(0, 1) * 100 * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
                else
                {
                    Direction = Direction.Left;
                }
                directionTimer -= 2.0;
            }

            if (Direction == Direction.Left)
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
            if(animationTimer > 0.3)
            {
                animationFrame++;
                if(animationFrame > 3)
                {
                    animationFrame = 1;
                }
                animationTimer -= 0.3;
            }
            var source = new Rectangle(animationFrame * 32, (int)Direction * 32, 32, 32);
            SpriteEffects spriteEffects = (flipped) ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
            spriteBatch.Draw(texture, Position, source, Color.White);
        }
    }
}
