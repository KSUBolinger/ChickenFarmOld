﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameProject0
{
    public class Game0 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        
        private SnakeSprite snake;
        private ChickenSprite chicken;
        private InputManager inputManager;

        public Game0()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            chicken = new ChickenSprite();
            snake = new SnakeSprite() { Position = new Vector2(200, 200), snakeDirection = SnakeDirection.Left };
            inputManager = new InputManager();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
           
            // TODO: use this.Content to load your game content here
            
            snake.LoadContent(Content);
            chicken.LoadContent(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            chicken.Update(gameTime);
            snake.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here
            spriteBatch.Begin();
            chicken.Draw(gameTime, spriteBatch);
            snake.Draw(gameTime, spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}