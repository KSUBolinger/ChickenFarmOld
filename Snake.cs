using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GameProject0
{
    public class Snake
    {
        Game game;

        Color color;

        Texture2D texture;

        public Vector2 Position { get; set; }

        public Snake(Game game)
        {
            this.game = game;

        }

        public void LoadContent()
        {
            texture = game.Content.Load<Texture2D>("Snake");
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (texture is null) throw new InvalidOperationException("Texture must be loaded to render");
            spriteBatch.Draw(texture, Position, color);
        }
    }
}
